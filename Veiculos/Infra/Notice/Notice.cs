using System;

namespace Veiculos.Infra.Notice
{
    public class Notice
    {

        public String Title { get; private set; }

        public String Message { get; private set; }

        public NoticeType Type { get; private set; }

        public Notice(String title, String message, NoticeType type)
        {
            Title = title;
            Message = message;
            Type = type;
        }

        public Notice(String message, NoticeType type)
            : this(null, message, type)
        {

        }

    }
}