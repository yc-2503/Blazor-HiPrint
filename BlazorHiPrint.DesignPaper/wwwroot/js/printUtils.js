// 导出PDF功能 (returns PDF blob)
export function exportToPdfAsBlob(elementId, width, height) {
    return new Promise((resolve, reject) => {
        // 动态加载html2canvas和jspdf库
        const loadScript = (url) => {
            return new Promise((resolve, reject) => {
                const script = document.createElement('script');
                script.src = url;
                script.onload = resolve;
                script.onerror = reject;
                document.head.appendChild(script);
            });
        };

        Promise.all([
            loadScript('_content/BlazorHiPrint.DesignPaper/js/html2canvas.min.js'),
            loadScript('_content/BlazorHiPrint.DesignPaper/js/jspdf.umd.min.js')
        ]).then(() => {
            const element = document.getElementById(elementId);
            if (!element) {
                reject('Element not found');
                return;
            }

            const { jsPDF } = window.jspdf;
            
            html2canvas(element, {
                scale: 2,
                logging: false,
                useCORS: true
            }).then(canvas => {
                const pdf = new jsPDF({
                    orientation: width > height ? 'l' : 'p',
                    unit: 'mm',
                    format: [width, height]
                });
                
                const imgData = canvas.toDataURL('image/png');
                const imgWidth = width;
                const imgHeight = (canvas.height * width) / canvas.width;
                
                pdf.addImage(imgData, 'PNG', 0, 0, imgWidth, imgHeight);
                const pdfBlob = pdf.output('blob');
                // Ensure we're returning a proper Blob object
                if (pdfBlob instanceof Blob) {
                    resolve(pdfBlob);
                } else {
                    // Fallback for environments where output('blob') doesn't return a proper Blob
                    const blob = new Blob([pdfBlob], { type: 'application/pdf' });
                    resolve(blob);
                }
            }).catch(reject);
        }).catch(reject);
    });
}

// 导出PDF功能 (legacy version that saves file)
export function exportToPdf(elementId, width, height) {
    return new Promise((resolve, reject) => {
        // 动态加载html2canvas和jspdf库
        const loadScript = (url) => {
            return new Promise((resolve, reject) => {
                const script = document.createElement('script');
                script.src = url;
                script.onload = resolve;
                script.onerror = reject;
                document.head.appendChild(script);
            });
        };

        Promise.all([
            loadScript('_content/BlazorHiPrint.DesignPaper/js/html2canvas.min.js'),
            loadScript('_content/BlazorHiPrint.DesignPaper/js/jspdf.umd.min.js')
        ]).then(() => {
            const element = document.getElementById(elementId);
            if (!element) {
                reject('Element not found');
                return;
            }

            html2canvas(element, {
                scale: 2,
                logging: false,
                useCORS: true
            }).then(canvas => {
                const { jsPDF } = window.jspdf;
                // 使用传入的尺寸创建PDF
                const pdf = new jsPDF({
                    orientation: width > height ? 'l' : 'p',
                    unit: 'mm',
                    format: [width, height]
                });
                const imgData = canvas.toDataURL('image/png');
                
                // 根据实际尺寸调整图像
                const pageWidth = width;
                const pageHeight = height;
                const imgWidth = pageWidth;
                const imgHeight = (canvas.height * pageWidth) / canvas.width;
                pdf.addImage(imgData, 'PNG', 0, 0, imgWidth, imgHeight);
                pdf.save('design.pdf');
                resolve();
            }).catch(reject);
        }).catch(reject);
    });
}

// 打印功能
// 发送PDF到打印服务
export async function sendPdfToServer(pdfBlob, filename) {
    const formData = new FormData();
    formData.append('file', pdfBlob, filename);
    
    try {
        const response = await fetch('http://localhost:5000/print?filename=' + encodeURIComponent(filename), {
            method: 'POST',
            body: formData
        });
        return response.ok;
    } catch (error) {
        console.error('Error sending PDF:', error);
        return false;
    }
}

// Combined function that generates PDF and sends to server
export async function generateAndSendPdf(elementId, width, height, filename) {
    try {
        // Load required libraries
        const loadScript = (url) => {
            return new Promise((resolve, reject) => {
                const script = document.createElement('script');
                script.src = url;
                script.onload = resolve;
                script.onerror = reject;
                document.head.appendChild(script);
            });
        };

        await Promise.all([
            loadScript('_content/BlazorHiPrint.DesignPaper/js/html2canvas.min.js'),
            loadScript('_content/BlazorHiPrint.DesignPaper/js/jspdf.umd.min.js')
        ]);

        const element = document.getElementById(elementId);
        if (!element) {
            console.error('Element not found');
            return false;
        }

        const { jsPDF } = window.jspdf;
        
        // Generate PDF
        const canvas = await html2canvas(element, {
            scale: 2,
            logging: false,
            useCORS: true
        });

        const pdf = new jsPDF({
            orientation: width > height ? 'l' : 'p',
            unit: 'mm',
            format: [width, height]
        });
        
        const imgData = canvas.toDataURL('image/png');
        const imgWidth = width;
        const imgHeight = (canvas.height * width) / canvas.width;
        
        pdf.addImage(imgData, 'PNG', 0, 0, imgWidth, imgHeight);
        const pdfBlob = pdf.output('blob');

        // Send to server
        const formData = new FormData();
        formData.append('file', pdfBlob, filename);
        
        const response = await fetch('http://localhost:5000/print?filename=' + encodeURIComponent(filename), {
            method: 'POST',
            body: formData,
            mode: 'no-cors'
        });
        return true;
    } catch (error) {
        console.error('Error in generateAndSendPdf:', error);
        return false;
    }
}

// 检查打印服务是否可用
export async function isPrintServiceAvailable() {
    try {
        const response = await fetch('http://localhost:5000/print/', {
            method: 'GET',
            mode: 'no-cors'
        });
        return true;
    } catch {
        return false;
    }
}

export function printContent(elementId, width, height) {
    return new Promise((resolve, reject) => {
        // 动态加载html2canvas库
        const loadScript = (url) => {
            return new Promise((resolve, reject) => {
                const script = document.createElement('script');
                script.src = url;
                script.onload = resolve;
                script.onerror = reject;
                document.head.appendChild(script);
            });
        };

        loadScript('_content/BlazorHiPrint.DesignPaper/js/html2canvas.min.js')
            .then(() => {
                const element = document.getElementById(elementId);
                if (!element) {
                    reject('Element not found');
                    return;
                }

                // 创建打印样式
                const style = document.createElement('style');
                // 将mm转换为px (1mm ≈ 3.78px)
                const widthPx = width * 3.78;
                const heightPx = height * 3.78;
                
                style.innerHTML = `
                    @page {
                        size: ${width}mm ${height}mm;
                        margin: 0;
                    }
                    @media print {
                        body {
                            margin: 0 !important;
                            padding: 0 !important;
                        }
                        body * {
                            visibility: hidden;
                        }
                        #print-container, #print-container * {
                            visibility: visible;
                        }
                        #print-container {
                            position: absolute;
                            left: 0;
                            top: 0;
                            width: ${widthPx}px;
                            height: ${heightPx}px;
                            transform-origin: 0 0;
                        }
                    }
                `;
                document.head.appendChild(style);

                console.log(`Print settings - Width: ${width}mm (${widthPx}px), Height: ${height}mm (${heightPx}px)`);
                console.log('Starting html2canvas rendering...');
                html2canvas(element, {
                    scale: 2,
                    logging: true,
                    useCORS: true
                }).then(canvas => {
                    console.log('html2canvas rendering completed');
                    
                    // 创建临时打印容器
                    const printContainer = document.createElement('div');
                    printContainer.id = 'print-container';
                    printContainer.style.width = `${widthPx}px`;
                    printContainer.style.height = `${heightPx}px`;
                    printContainer.style.overflow = 'hidden';
                    printContainer.style.boxSizing = 'border-box';
                    
                    // 调整canvas尺寸匹配打印尺寸
                    canvas.style.width = '100%';
                    canvas.style.height = 'auto';
                    printContainer.appendChild(canvas);
                    document.body.appendChild(printContainer);

                    console.log('Print container created, triggering print...');
                    
                    // 使用setTimeout确保DOM更新完成
                    setTimeout(() => {
                        window.print();
                        console.log('Print dialog opened');

                        // 监听afterprint事件进行清理
                        window.addEventListener('afterprint', () => {
                            console.log('Print completed, cleaning up...');
                            document.body.removeChild(printContainer);
                            document.head.removeChild(style);
                            resolve();
                        }, { once: true });
                    }, 500);
                }).catch(err => {
                    console.error('html2canvas error:', err);
                    reject(err);
                });
            }).catch(reject);
    });
}
