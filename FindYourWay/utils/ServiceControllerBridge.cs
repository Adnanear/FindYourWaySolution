namespace FindYourWay.utils
{
  public class ServiceControllerBridge<T>
  {

    public int code { get; set; }
    public T? response { get; set; }

    public ServiceControllerBridge(int code, T? response = default(T))
    {
      this.code = code;
      this.response = response;
    }

  }
}