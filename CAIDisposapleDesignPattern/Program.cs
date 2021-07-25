using System;
using System.Net.Http;

namespace CAIDisposapleDesignPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1) not recommended
            //CurrencyService currencyService = new CurrencyService();
            //var result = currencyService.GetCurrencies();
            //currencyService.Dispose();
            //Console.WriteLine(result);

            //2) recommended
            //CurrencyService currencyService = null;
            //try
            //{
            //    currencyService = new CurrencyService();
            //    var result = currencyService.GetCurrencies();
            //    Console.WriteLine(result);

            //}
            //catch (Exception)
            //{
            //    Console.WriteLine("Error");
            //}
            //finally
            //{
            //    currencyService?.Dispose(); 
            //}

            // 3) more recommended  using .net framework 2+

            //using (CurrencyService currencyService = new CurrencyService())
            //{ 
            //    var result = currencyService.GetCurrencies();
            //    Console.WriteLine(result);
            //}

            // 4) using with no blocks c# 8.0
            CurrencyService currencyService = new CurrencyService();
            var result = currencyService.GetCurrencies();
            Console.WriteLine(result);
            Console.ReadKey();
        }
    }

    class CurrencyService : IDisposable
    {
        private readonly HttpClient httpClient;
        private bool _disposed = false;
        public CurrencyService()
        {
            httpClient = new HttpClient();
        }

        ~CurrencyService()
        {
            Dispose(false);
        }

        // disposing : true (dispose managed + unmanaged)      
        // disposing : false (dispose unmanaged + large fields)
        protected virtual void Dispose (bool disposing)
        {
            if (_disposed)
                return;

            // Dispose Logic
            if(disposing)
            {
                // dispose managed resouces
                httpClient.Dispose();
            }
            // unmanaged object
            // set large fields to null
            _disposed = true;

        }

        public void Dispose()
        {
            // dipose() is called 100%
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public string GetCurrencies()
        {
            string url = "https://coinbase.com/api/v2/currencies";
            var result = httpClient.GetStringAsync(url).Result;
            return result;
        }
    }
}
