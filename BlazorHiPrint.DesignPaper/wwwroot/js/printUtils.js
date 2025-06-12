// 导出PDF功能
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
