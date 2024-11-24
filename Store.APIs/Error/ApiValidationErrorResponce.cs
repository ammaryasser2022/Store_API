namespace Store.APIs.Error
{
    public class ApiValidationErrorResponce : ApiErrorResponse
    {
        public IEnumerable<string> Errors { get; set; } = new List<string>();

        //Dont foget that any class inherit from another by default his ctor chain on parameterless ctor in parent 
        // i need to chain on parameter ctor 

        public ApiValidationErrorResponce() : base(400)
        {
            
        }

        // i need to told the service about this class or this new Configrations

    }
}
