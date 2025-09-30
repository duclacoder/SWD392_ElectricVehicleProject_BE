using EV.Application.ResponseDTOs;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV.Application.Interfaces.ServiceInterfaces
{
    public interface IModelStateCheck
    {
        ResponseDTO<T> CheckModelState<T>(ModelStateDictionary modelState);
    }
}
