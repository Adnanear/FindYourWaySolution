namespace FindYourWay.utils
{
    // This is a generic class to help us share response from services to controllers and vise-versa
  public class ServiceControllerWrapper<T>
  {

        // Status code: HTTP Codes
    public int code { get; set; }

        // response: the actual response
    public T? response { get; set; }

        // Construct the bridge
    public ServiceControllerWrapper(int code, T? response = default(T))
    {

            // fill the fields
      this.code = code;
      this.response = response;
    }

  }
}