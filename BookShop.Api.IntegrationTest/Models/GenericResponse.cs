namespace BookShop.Api.IntegrationTest.Models
{
    public class GenericResponse<T> where T : class
    {
        public T Data { get; set; }

        /// <summary>
        ///
        /// </summary>
        public bool IsSuccessful { get; }

        /// <summary>
        ///
        /// </summary>
        public string Message { get; }
    }
}