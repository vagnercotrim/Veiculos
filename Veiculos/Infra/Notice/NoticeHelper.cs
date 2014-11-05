using System.Collections.Generic;
using FluentNHibernate.Conventions;
using FluentNHibernate.Utils;
using Veiculos.Infra.Notice;

namespace System.Web.Mvc
{
    public static class NoticeHelper
    {

        public static IDictionary<String, NoticeType> GetNoticesFromTempData(this ControllerBase controller)
        {
            var notices = controller.TempData["notices"];

            if (notices == null)
                return new Dictionary<String, NoticeType>();

            return (Dictionary<String, NoticeType>)notices;
        }

        public static void ClearNotices(this ControllerBase controller)
        {
            controller.TempData["notices"] = new Dictionary<String, NoticeType>();
        }

        public static void AddNotice(this ControllerBase controller, IDictionary<String, NoticeType> erros)
        {
            controller.TempData["notices"] = erros;
        }

        public static void AddNotice(this ControllerBase controller, string mensagem, NoticeType classe)
        {
            try
            {
                IDictionary<String, NoticeType> notices = GetNoticesFromTempData(controller);
                notices.Add(mensagem, classe);
                AddNotice(controller, notices);
            }
            catch { }
        }

        public static void Success(this ControllerBase controller, string mensagem)
        {
            AddNotice(controller, mensagem, NoticeType.Success);
        }

        public static void Info(this ControllerBase controller, string mensagem)
        {
            AddNotice(controller, mensagem, NoticeType.Info);
        }

        public static void Warning(this ControllerBase controller, string mensagem)
        {
            AddNotice(controller, mensagem, NoticeType.Warning);
        }

        public static void Error(this ControllerBase controller, string mensagem)
        {
            AddNotice(controller, mensagem, NoticeType.Error);
        }

    }
}