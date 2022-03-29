using Microsoft.AspNetCore.Mvc;
using prmToolkit.NotificationPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TesteCitel.Domain.Interfaces.Services.Base;
using TesteCitel.Infra.Transactions;

namespace TesteCitel.WEB.Controllers.Base
{
    public class BaseController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private IServiceBase _serviceBase;

        public BaseController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [NonAction]
        protected Task ResponseAsync(IServiceBase serviceBase)
        {
            _serviceBase = serviceBase;

            if (!_serviceBase.Notifications.Any())
            {
                try
                {
                    _unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    throw new Exception($"Houve um problema interno com o servidor. Entre em contato com o nosso suporte caso o problema persista. Erro interno: {ex.Message}");
                }
            }
            return Task.CompletedTask;
        }

        protected void AddModelError(IReadOnlyCollection<Notification> notifications)
        {
            foreach (var notification in notifications)
            {
                ModelState.AddModelError(notification.Property, notification.Message);
            }
        }

        protected string GetMessageErrors(IReadOnlyCollection<Notification> notifications)
        {
            var message = string.Empty;
            foreach (var notification in notifications)
            {
                message += notification.Message;
            }

            return message;
        }

        protected IActionResult ModelStateErrors()
        {
            var errors = ModelState
                .Where(x => x.Value.Errors.Count > 0)
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).FirstOrDefault()
                );

            var notifications = new List<Notification>();

            foreach (var error in errors)
            {
                notifications.Add(new Notification(error.Key, error.Value));
            }

            return BadRequest(new { errors = notifications });
        }
    }
}
