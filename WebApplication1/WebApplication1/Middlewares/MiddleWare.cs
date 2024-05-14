namespace WebApplication1.Middlewares
{
    public class MiddleWare
    {
        private readonly RequestDelegate _next;
        public MiddleWare(RequestDelegate next) 
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext) 
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex) 
            {
                string filePath = @"C:\Users\Patryk\source\repos\cwiczenia8_mp-PatrykNosal66\WebApplication1\WebApplication1\logs.txt";


                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine("-----------------------------------------------------------------------------");
                    writer.WriteLine("Date : " + DateTime.Now.ToString());
                    writer.WriteLine();

                    while (ex != null)
                    {
                        writer.WriteLine(ex.GetType().FullName);
                        writer.WriteLine("Message : " + ex.Message);
                        writer.WriteLine("StackTrace : " + ex.StackTrace);

                        ex = ex.InnerException;
                    }
                }
            }
        }
    }
}
