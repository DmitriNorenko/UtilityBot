using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace bot.Controllers
{
    public class TextMessageController
    {
        private readonly ITelegramBotClient _telegramClient;

        public TextMessageController(ITelegramBotClient telegramBotClient)
        {
            _telegramClient = telegramBotClient;
        }
        public async Task Handle(Message message, CancellationToken ct)
        {
            switch (message.Text)
            {
                case "/start":
                    var buttons = new List<InlineKeyboardButton[]>();
                    buttons.Add(new[]
                    {
                        InlineKeyboardButton.WithCallbackData($"Сложение" , $"+"),
                        InlineKeyboardButton.WithCallbackData($"К.Букв" , $"*")
                    });
                    await
                        _telegramClient.SendTextMessageAsync(message.Chat.Id, $"<b>" +
                        $"Бот складывает числа или посчитывает количество символов.</b> " +
                        $"{Environment.NewLine}", cancellationToken: ct,
                        parseMode: ParseMode.Html, replyMarkup: new InlineKeyboardMarkup(buttons));
                    break;
                default:
                    await
                        _telegramClient.SendTextMessageAsync(message.Chat.Id,
                        "Напишите сообщение", cancellationToken: ct);
                    break;
            }
        }
    }
}
