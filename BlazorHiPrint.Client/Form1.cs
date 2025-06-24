using PdfiumViewer;
using System.Drawing.Printing;
using System.Net;

namespace BlazorHiPrint.Client
{
    /// <summary>
    /// Main form for the BlazorHiPrint client application that handles PDF printing requests
    /// from a Blazor web application via HTTP and manages print queue processing.
    /// </summary>
    public partial class Form1 : Form
    {
        // HTTP listener for receiving print requests from web clients
        private HttpListener _listener;
        
        // Timer for processing pending print jobs
        private System.Windows.Forms.Timer _printTimer;
        
        // Timer for scanning print queue and updating UI
        private System.Windows.Forms.Timer _scanTimer;
        
        // Temporary directory for storing PDF files to be printed
        private static readonly string TempPrintFolder = Path.Combine(Path.GetTempPath(), "BlazorHiPrint");
        
        // Queue of files that have been successfully printed
        private readonly Queue<string> _printedFiles = new Queue<string>();
        
        // Queue of files waiting to be printed
        private readonly Queue<string> _waitPrintFiles = new Queue<string>();
        /// <summary>
        /// Initializes the form and sets up:
        /// - Temporary print directory
        /// - HTTP listener for print requests
        /// - Timers for processing print jobs and scanning queue
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            
            // Create temp directory if not exists
            Directory.CreateDirectory(TempPrintFolder);
            
            // Configure HTTP listener to receive print requests
            _listener = new HttpListener();
            _listener.Prefixes.Add("http://localhost:5000/print/");
            _listener.Start();
            _listener.BeginGetContext(OnPrintRequest, null);
            
            // Setup print timer to process pending jobs every 3 seconds
            _printTimer = new System.Windows.Forms.Timer();
            _printTimer.Interval = 3000;
            _printTimer.Tick += OnPrintTimerTick;
            _printTimer.Start();

            // Setup scan timer to update UI queue every 2 seconds
            _scanTimer = new System.Windows.Forms.Timer();
            _scanTimer.Interval = 2000;
            _scanTimer.Tick += OnScanTimerTick;
            _scanTimer.Start();
        }

        /// <summary>
        /// Handles incoming HTTP print requests from web clients
        /// </summary>
        /// <param name="ar">Async result from HTTP listener</param>
        private async void OnPrintRequest(IAsyncResult ar)
        {
            try
            {
                var context = _listener.EndGetContext(ar);
                var request = context.Request;
                var response = context.Response;

                // Only process POST requests with content
                if (request.HttpMethod == "POST" && request.InputStream != null)
                {
                    string? fileName = request.QueryString["filename"];
                    if (fileName == null)
                    {
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        response.Close();
                        return;
                    }
                    
                    // Save PDF to temp file with unique GUID prefix
                    string tempFilePath = Path.Combine(TempPrintFolder, $"{Guid.NewGuid()}_{fileName}");
                    using (var fileStream = File.Create(tempFilePath))
                    {
                        await request.InputStream.CopyToAsync(fileStream);
                    }

                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Close();
                }else if (request.HttpMethod == "GET")
                {
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Close();
                }
                else
                {
                    Console.WriteLine("Invalid request method or no content received.");
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    response.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing print request: {ex.Message}");
            }
            finally
            {
                // Continue listening for new requests
                _listener.BeginGetContext(OnPrintRequest, null);
            }
        }

        /// <summary>
        /// Form load event handler - initializes default printer display
        /// </summary>
        private void Form1_Load(object sender, EventArgs e)
        {
            PrintDocument print = new PrintDocument();
            string sDefault = print.PrinterSettings.PrinterName;
            tslPrinter.Text = sDefault;
        }
        /// <summary>
        /// Timer callback that processes pending print jobs from the queue
        /// </summary>
        private void OnPrintTimerTick(object sender, EventArgs e)
        {
            if(_waitPrintFiles.Count == 0)
            {
                return;
            }
            
            // Disable timer during processing to prevent re-entrancy
            _printTimer.Enabled = false;
            try
            {
                string? fileFullName = string.Empty;
                while(_waitPrintFiles.TryDequeue(out fileFullName))
                {
                    if (File.Exists(fileFullName))
                    {
                        // Print the PDF using PdfiumViewer
                        using (var doc = PdfDocument.Load(fileFullName))
                        {
                            var pdoc = doc.CreatePrintDocument();
                            pdoc.Print();
                        }
                    }
                    
                    // Move file to printed queue and delete from disk
                    File.Delete(fileFullName);
                    _printedFiles.Enqueue(fileFullName);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Print error: {ex.Message}");
            }
            finally
            {
                // Re-enable timer after processing
                _printTimer.Enabled = true;
            }
        }
        /// <summary>
        /// Timer callback that scans the print queue and updates the UI
        /// </summary>
        private void OnScanTimerTick(object sender, EventArgs e)
        {
            _scanTimer.Enabled = false;
            try 
            {
                // Process completed prints - remove from UI
                if(_printedFiles.Count>0)
                {
                    string? fileFullName = string.Empty;
                    while (_printedFiles.TryDequeue(out fileFullName))
                    {
                        var fileTask = Path.GetFileName(fileFullName).Split("_").First();

                        // Find and remove matching row from DataGridView
                        int removeIdx = -1;
                        for (int i = 0; i < dgvFiles.Rows.Count; i++)
                        {
                            DataGridViewRow row = dgvFiles.Rows[i];
                            var id = row.Cells[0].Value!.ToString();
                            if (id == fileTask)
                            {
                                removeIdx = i;
                                break;
                            }
                        }
                        if (removeIdx >= 0)
                        {
                            dgvFiles.Rows.RemoveAt(removeIdx);
                        }
                    }
                }
                
                // Scan temp directory for new print jobs
                foreach (var queueFile in Directory.GetFiles(TempPrintFolder))
                {
                    if (Path.GetExtension(queueFile) == ".pdf")
                    {
                        var fileInfo = new FileInfo(queueFile);
                        var fileTask = fileInfo.Name.Split("_").First();
                        var fileName = fileInfo.Name.Replace(fileTask + "_", "");
                        
                        // Check if file is already in queue
                        bool exists = false;
                        foreach (DataGridViewRow row in dgvFiles.Rows)
                        {
                            var id = row.Cells[0].Value!.ToString();
                            if (id == fileTask)
                            {
                                exists = true;
                                break;
                            }
                        }
                        
                        // Add new files to queue
                        if (!exists)
                        {
                            dgvFiles.Rows.Add(fileTask, fileName, fileInfo.CreationTime, "等待打印");
                            _waitPrintFiles.Enqueue(queueFile);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Queue scan error: {ex.Message}");
            }
            finally
            {
                _scanTimer.Enabled = true;
            }
        }
        
        /// <summary>
        /// Clean up resources when form is closing
        /// </summary>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // Stop and dispose timers
            _scanTimer.Stop();
            _scanTimer.Dispose();
            _printTimer?.Stop();
            _printTimer?.Dispose();
            base.OnFormClosing(e);
        }
    }
}
