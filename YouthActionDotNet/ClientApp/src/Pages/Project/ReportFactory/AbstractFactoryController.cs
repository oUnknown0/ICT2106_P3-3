public class AbstractFactoryController {
   public static IReportFactory generate(string reportType){   
      if(reportType == "Docx"){
         return new DocxReportFactory();         
      }
      else if(reportType == "PDF"){
         return new PDFReportFactory();
      }
      else if(reportType == "XLS"){
         return new XLSReportFactory();
      }
      else{
         return null;
      }
   }
}