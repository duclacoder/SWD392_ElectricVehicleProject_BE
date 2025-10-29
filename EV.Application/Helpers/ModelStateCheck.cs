using EV.Application.Interfaces.ServiceInterfaces;
using EV.Application.ResponseDTOs;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EV.Application.Helpers
{
    public class ModelStateCheck : IModelStateCheck
    {
        public ResponseDTO<T> CheckModelState<T>(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
            {
                var errors = modelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                var errorMessage = string.Join("; ", errors);

                return new ResponseDTO<T>(errorMessage, false, default);
            }

            return new ResponseDTO<T>("Model is valid", true, default);
        }
    }
}
