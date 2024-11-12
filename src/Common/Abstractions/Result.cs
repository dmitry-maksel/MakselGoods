namespace Abstractions;
public abstract class Result<T> where T: class
{
    public bool Success => Status == ResultStatus.Success;

    public ResultStatus Status { get; } = ResultStatus.Success;

    public T Data { get; set; } = default!;

    public List<string> ErorrMessages { get; } = [];
}
