using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registeration.BL;

public class OperationResult<T>
{

    public bool Success { get; set; }
    public T? Data { get; set; }
    public string? ErrorMessage { get; set; }

    public OperationResult(T? data)
    {
        Success = true;
        Data = data;
    }

    public OperationResult(string errorMessage)
    {
        Success = false;
        ErrorMessage = errorMessage;
    }

}
