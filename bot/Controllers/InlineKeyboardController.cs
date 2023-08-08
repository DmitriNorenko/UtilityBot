using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Telegram.Bot;
using bot.Models;

 namespace bot.Controllers
{
    public class InlineKeyboardController
    {
        private readonly IStorage _memoryStorage;
        private readonly ITelegramBotClient _telegramClient;

        public InlineKeyboardController(ITelegramBotClient telegramBotClient,
            IStorage memoryStorage)
        {
            _telegramClient = telegramBotClient;
            _memoryStorage = memoryStorage;
        }
        public async Task Handle(CallbackQuery? callbackQuery, CancellationToken ct)
        {
            if (callbackQuery?.Data == null) return;

            _memoryStorage.GetSession(callbackQuery.From.Id).ActionCode = callbackQuery.Data;

            string action = callbackQuery.Data switch
            {
                "+" => "Сложение",
                "*" => "Количество символов",
                _ => String.Empty
            };

            await
                _telegramClient.SendTextMessageAsync(callbackQuery.From.Id,
                $"<b>Вы выбрали - {action}.{Environment.NewLine}</b>" +
                $"{Environment.NewLine} Можете уже использовать бота!", cancellationToken: ct,
                parseMode: ParseMode.Html);
        }
    }
}
