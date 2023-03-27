namespace FindYourWay.utils
{
  public class ServiceControllerWrapper<T>
  {

    public int code { get; set; }
    public T? response { get; set; }

    public ServiceControllerWrapper(int code, T? response = default(T))
    {
      this.code = code;
      this.response = response;
    }

  }
}