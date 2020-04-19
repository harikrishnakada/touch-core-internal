namespace touch_core_internal.Services
{
    public class ServiceResponse<T>
    {
        public T Data { get; set; }

        public string Message { get; set; }

        public bool Status { get; set; }

        public void UpdateResponseStatus(string message, bool status = true)
        {
            this.Message = message;
            this.Status = status;
        }
    }
}