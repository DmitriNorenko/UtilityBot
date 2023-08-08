using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using bot.Models;
using bot.Controllers;
using System.Threading;

namespace bot.Controllers
{
    public class TextMessageController
    {
        private readonly IStorage _memoryStorage;
        private readonly ITelegramBotClient _telegramClient;

        public TextMessageController(ITelegramBotClient telegramBotClient, IStorage memoryStorage)
        {
            _memoryStorage = memoryStorage;
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
                    string userLanguageCode = _memoryStorage.GetSession(message.Chat.Id).ActionCode;
                    if (userLanguageCode == "*")
                    {
                        await _telegramClient.SendTextMessageAsync(message.Chat.Id,
                        $"Длина сообщения: {message.Text.Length}.", cancellationToken: ct);
                    }

                    break;
            }
        }
    }
}
