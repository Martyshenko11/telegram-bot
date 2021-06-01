using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.Enums;

namespace NYT_Bot1
{
    class Program
    {
        private static string Token { get; set; } = "1700337529:AAEIIXfRkwCmpXU0zId0oSTDTyO3HuYADo8";
        private static TelegramBotClient client;

        static void Main(string[] args)
        {        
            client = new TelegramBotClient(Token);
            client.StartReceiving();
            client.OnMessage += OnMessageHandler;
            client.OnCallbackQuery += ClientOnCallbackQuery;
            client.OnCallbackQuery += ClientOnCallbackQuery1;
            Console.ReadLine();
            client.StopReceiving();         
        }

        public static async void OnMessageHandler(object sender, MessageEventArgs e)
        {
            string apiAddress = "https://webapplication120210531230754.azurewebsites.net/";
            int i = 1;

            var msg = e.Message;
            if (msg.Text != null)
            {
                Console.WriteLine($"Username:{e.Message.From.Username} Name:{e.Message.From} Message: {msg.Text} ");           
                switch (msg.Text)
                {
                    
                    case "📰 Most popular":

                        var client1 = new HttpClient();
                        client1.BaseAddress = new Uri(apiAddress);

                        var result = await client1.GetAsync($"article/mostpopular");
                        try
                        {
                            result.EnsureSuccessStatusCode();
                        }
                        catch (System.Net.Http.HttpRequestException)
                        {
                            Console.WriteLine("System.Net.Http.HttpRequestException");
                        }
                        var content = result.Content.ReadAsStringAsync().Result;

                        try
                        {
                            var response = JsonConvert.DeserializeObject<Response[]>(content);
                            try
                            {
                                if (response == null)
                                {
                                    Console.WriteLine("response6 == null");
                                }
                                else
                                {
                                    await client.SendTextMessageAsync(msg.Chat.Id, $"Top 10 Most Popular Articles:\n\n" +
                             $"{i} - {response[0].Title}\n\n{i + 1} - {response[1].Title}\n\n" +
                             $"{i + 2} - {response[2].Title}\n\n{i + 3} - {response[3].Title}\n\n{i + 4} - {response[4].Title}\n\n" +
                             $"{i + 5} - {response[5].Title}\n\n{i + 6} - {response[6].Title}\n\n{i + 7} - {response[7].Title}\n\n" +
                             $"{i + 8} - {response[8].Title}\n\n{i + 9} - {response[9].Title}\n\n");
                                }
                            }
                            catch (Telegram.Bot.Exceptions.ApiRequestException)
                            {
                                Console.WriteLine("Telegram.Bot.Exceptions.ApiRequestException");
                             
                            }
                            try
                            {
                                await client.SendTextMessageAsync(msg.Chat.Id, "For more detailed information enter article number:", replyMarkup: NewOrderKeyboardFirst);
                            }
                            catch (Telegram.Bot.Exceptions.ApiRequestException)
                            {
                                Console.WriteLine("Telegram.Bot.Exceptions.ApiRequestException");

                            }
                        }
                        catch (Newtonsoft.Json.JsonReaderException)
                        {
                            Console.WriteLine("Newtonsoft.Json.JsonReaderException");
                            await client.SendTextMessageAsync(msg.Chat.Id, "There is something wrong, please repeat your request.", replyMarkup: GetButtons());
                        }
                        break;

                    case "📚 Books":

                        var client3 = new HttpClient();
                        client3.BaseAddress = new Uri(apiAddress);

                        var result3 = await client3.GetAsync($"article/books");
                        try
                        {
                            result3.EnsureSuccessStatusCode();
                        }
                        catch (System.Net.Http.HttpRequestException)
                        {
                            Console.WriteLine("System.Net.Http.HttpRequestException");
                        }
                        var content3 = result3.Content.ReadAsStringAsync().Result;

                        try 
                        { 
                        var response3 = JsonConvert.DeserializeObject<Response3[]>(content3);
                            try
                            {
                                if (response3 == null)
                                {
                                    Console.WriteLine("response6 == null");
                                }
                                else
                                {
                                    await client.SendTextMessageAsync(msg.Chat.Id, $"Top 10 bestsellers of the week:\n\n" +
                                    $"{i} - {response3[0].title}\n\n{i + 1} - {response3[1].title}\n\n" +
                                    $"{i + 2} - {response3[2].title}\n\n{i + 3} - {response3[3].title}\n\n{i + 4} - {response3[4].title}\n\n" +
                                    $"{i + 5} - {response3[5].title}\n\n{i + 6} - {response3[6].title}\n\n{i + 7} - {response3[7].title}\n\n" +
                                    $"{i + 8} - {response3[8].title}\n\n{i + 9} - {response3[9].title}\n\n", replyMarkup: GetButtons());
                                }
                            }
                            catch (Telegram.Bot.Exceptions.ApiRequestException)
                            {
                                Console.WriteLine("Telegram.Bot.Exceptions.ApiRequestException");
                               
                            }
                        await client.SendTextMessageAsync(msg.Chat.Id, "For more detailed information use the book number:", replyMarkup: NewOrderKeyboardFirst1);
                        }
                        catch (Newtonsoft.Json.JsonReaderException)
                        {
                            Console.WriteLine("Newtonsoft.Json.JsonReaderException");
                            await client.SendTextMessageAsync(msg.Chat.Id, "There is something wrong, please repeat your request.", replyMarkup: GetButtons());
                        }
                        break;

                    case "💬 Community":
                        try
                        {
                            var result7 = client.SendTextMessageAsync(msg.Chat.Id, $"Enter the link of the article:", replyMarkup: new ForceReplyMarkup() { Selective = true }).Result;
                        }
                        catch (System.AggregateException)
                        {
                            Console.WriteLine("System.AggregateException");

                        }
                        break;

                    case "🗞 Top Stories":
                        try
                        {
                            await client.SendTextMessageAsync(msg.Chat.Id, $"Available section list: arts, automobiles, books, business, fashion, food, health, home, " +
                                $"insider, magazine, movies, nyregion, obituaries, opinion, politics, realestate, science, " +
                                $"sports, sundayreview, technology, theater, t-magazine, travel, upshot, us, and world.");
                        }
                        catch (Telegram.Bot.Exceptions.ApiRequestException)
                        {
                            Console.WriteLine("Telegram.Bot.Exceptions.ApiRequestException");
                            
                        }
                        try
                        {
                            var result6 = client.SendTextMessageAsync(msg.Chat.Id, "Enter the name of the desired section:", replyMarkup: new ForceReplyMarkup() { Selective = true }).Result;
                        }
                        catch (System.AggregateException)
                        {
                            Console.WriteLine("System.AggregateException");
    
                        }
                        break;

                    case "📽 Review films":
                        try
                        {
                            var result4 = client.SendTextMessageAsync(msg.Chat.Id, $"Enter the title of the desired movie:", replyMarkup: new ForceReplyMarkup() { Selective = true }).Result;
                        }
                        catch (System.AggregateException)
                        {
                            Console.WriteLine("System.AggregateException");
                        }
                        break;

                    case "🗝 Instructions":
                        try
                        {
                            await client.SendTextMessageAsync(msg.Chat.Id, $"Instructions for using the bot:\n\n" +
                                $"📚 Books - Top 10 most popular bestsellers of the last week.\n\n" +
                                $"📰 Most popular - Top 10 most popular articles.\n\n" +
                                $"🗞 Top Stories - Top 5 most popular articles of the last day. Recommended Top 5 articles from the bot. Articles are divided " +
                                $"into given sections: arts, automobiles, books, business, fashion, food, health, home, " +
                                $"insider, magazine, movies, nyregion, obituaries, opinion, politics, realestate, science, " +
                                $"sports, sundayreview, technology, theater, t-magazine, travel, upshot, us, and world. To get articles, " +
                                $"write the title of the desired section. You will receive the newest options. You can only get one category at a time, " +
                                $"for example, art.\n\n" +
                                $"📽 Review films - New York Times movie reviews by critics. To get possible articles, " +
                                $"write the title of the desired movie. You will get all possible options. If the articles are not displayed, " +
                                $"then they are missing for this movie.\n\n" +
                                $"💬 Community - Comments from registered users on New York Times articles. To get comments, paste the link " +
                                $"to the desired article. You will get all possible options. If comments are not displayed, " +
                                $"then they are missing for this article.\n\n" +
                                $"/buttons - Command for calling buttons.\n\n" +
                                $"📌 All further instructions for using the bot will be described after pressing the buttons. " +
                                $"If the button does not work after the first press, please wait and press it again. " +
                                $"If the bot stops working, try restarting it. If you have any problems, write here: bandersonjack80@gmail.com.", replyMarkup: GetButtons());
                        }
                        catch (Telegram.Bot.Exceptions.ApiRequestException)
                        {
                            Console.WriteLine("Telegram.Bot.Exceptions.ApiRequestException");
                        }
                        break;

                    case "/buttons":
                        await client.SendTextMessageAsync(msg.Chat.Id, "Select the command:", replyMarkup: GetButtons());
                        break;

                   default:
                        try
                        {
                            await client.SendTextMessageAsync(msg.Chat.Id, "Found on your request:", replyMarkup: GetButtons());
                        }
                        catch (Telegram.Bot.Exceptions.ApiRequestException)
                        {
                            Console.WriteLine("Telegram.Bot.Exceptions.ApiRequestException");
                        }
                       break; 
                }
               
            }

            if (e.Message.ReplyToMessage != null && e.Message.ReplyToMessage.Text.Contains("Enter the name of the desired section:"))
            {
                var client6 = new HttpClient();
                client6.BaseAddress = new Uri(apiAddress);
                var result6 = await client6.GetAsync($"article/topstories?Name={e.Message.Text}");
                
                    try
                { 
                    result6.EnsureSuccessStatusCode();
                }
                catch (System.Net.Http.HttpRequestException)
                {
                    Console.WriteLine("System.Net.Http.HttpRequestException");
                    await client.SendTextMessageAsync(msg.Chat.Id, "No results were found for your search.", replyMarkup: GetButtons());                   
                }
                        var content6 = result6.Content.ReadAsStringAsync().Result;

                        try
                        {
                            var response6 = JsonConvert.DeserializeObject<Response6[]>(content6);
                    if (response6 == null)
                    {
                        Console.WriteLine("response6 == null");
                    }
                    else
                    {
                        for (int j = 1; j < 6; j++)
                        {
                            await client.SendTextMessageAsync(msg.Chat.Id, $"{response6[j - 1].Url}\n\nTitle: {response6[j - 1].Title}\n", replyMarkup: GetButtons());
                        }
                    }
                        }
                        catch (Newtonsoft.Json.JsonReaderException)
                        {
                            Console.WriteLine("Newtonsoft.Json.JsonReaderException");
                        }
                    }
                else if (e.Message.ReplyToMessage != null && e.Message.ReplyToMessage.Text.Contains("Enter the title of the desired movie:"))
                {
                    var client4 = new HttpClient();
                    client4.BaseAddress = new Uri(apiAddress);
                    var result4 = await client4.GetAsync($"article/reviewfilms?Name={e.Message.Text}");
                    try
                    {
                        result4.EnsureSuccessStatusCode();
                    }
                    catch (System.Net.Http.HttpRequestException)
                    {
                        Console.WriteLine("System.Net.Http.HttpRequestException");
                        await client.SendTextMessageAsync(msg.Chat.Id, "No results were found for your search", replyMarkup: GetButtons());
                    }
                    var content4 = result4.Content.ReadAsStringAsync().Result;

                    try
                    {
                        var response4 = JsonConvert.DeserializeObject<Response4[]>(content4);
                    if (response4 == null)
                    {
                        Console.WriteLine("response4 == null");
                    }
                    else
                    {
                        for (int j = 1; j < response4.Length + 1; j++)
                        {
                            await client.SendTextMessageAsync(msg.Chat.Id, $"{response4[j - 1].link.url}", replyMarkup: GetButtons());
                        }
                    }
                    }
                    catch (Newtonsoft.Json.JsonReaderException)
                    {
                        Console.WriteLine("Newtonsoft.Json.JsonReaderException");
                    }
                }
                else if (e.Message.ReplyToMessage != null && e.Message.ReplyToMessage.Text.Contains("Enter the link of the article:"))
                {
                    var client7 = new HttpClient();
                    client7.BaseAddress = new Uri(apiAddress);
                    var result7 = await client7.GetAsync($"article/comments?Name={e.Message.Text}");
                    try
                    {
                        result7.EnsureSuccessStatusCode();
                    }
                    catch (System.Net.Http.HttpRequestException)
                    {
                        Console.WriteLine("System.Net.Http.HttpRequestException");
                        await client.SendTextMessageAsync(msg.Chat.Id, "No results were found for your search", replyMarkup: GetButtons());
                    }
                    var content7 = result7.Content.ReadAsStringAsync().Result;

                    try
                    {
                        var response7 = JsonConvert.DeserializeObject<Response7[]>(content7);
                    if (response7 == null)
                    {
                        Console.WriteLine("response7 == null");
                    }
                    else
                    {
                        for (int j = 1; j < response7.Length + 1; j++)

                        {
                            await client.SendTextMessageAsync(msg.Chat.Id, $"👤 User name: {response7[j - 1].userDisplayName}\n" +
                                $"🌆 User location: {response7[j - 1].userLocation}\n💬 Сomment: {response7[j - 1].commentBody}", replyMarkup: GetButtons());
                        }
                    }
                    }
                    catch (Newtonsoft.Json.JsonReaderException)
                    {
                        Console.WriteLine("Newtonsoft.Json.JsonReaderException");
                    }
                }
            }


        private static IReplyMarkup GetButtons()
        {
            return new ReplyKeyboardMarkup
            {
                Keyboard = new[] {
        new[]{
            new KeyboardButton("📚 Books"),
            new KeyboardButton("📰 Most popular")
        },      
         new[]{
            new KeyboardButton("🗞 Top Stories"),
            new KeyboardButton("📽 Review films")
         },
          new[]{
            new KeyboardButton("🗝 Instructions"),
            new KeyboardButton("💬 Community")
        },
     },
                ResizeKeyboard = true
            };
        }

        async static void ClientOnCallbackQuery(object sender, CallbackQueryEventArgs ev)
        {
            try
            {
                string apiAddress = "https://webapplication120210531230754.azurewebsites.net/";

            var client1 = new HttpClient();
            client1.BaseAddress = new Uri(apiAddress);

            var result = await client1.GetAsync($"article/mostpopular");
            result.EnsureSuccessStatusCode();
            var content = result.Content.ReadAsStringAsync().Result;

            var response = JsonConvert.DeserializeObject<Response[]>(content);

            var message = ev.CallbackQuery.Message;
            if (ev.CallbackQuery.Data == "1M")
            {
                await client.SendTextMessageAsync(chatId: ev.CallbackQuery.Message.Chat, $"{response[0].Url}");
                    try
                    {
                        await client.AnswerCallbackQueryAsync(ev.CallbackQuery.Id);
                    }
                    catch(Telegram.Bot.Exceptions.InvalidParameterException)
                    {
                        Console.WriteLine("Telegram.Bot.Exceptions.InvalidParameterException");
                        await client.SendTextMessageAsync(chatId: ev.CallbackQuery.Message.Chat, "There is something wrong, please repeat your request.", replyMarkup: GetButtons());
                    }
                }
            else if (ev.CallbackQuery.Data == "2M")
            {
                await client.SendTextMessageAsync(chatId: ev.CallbackQuery.Message.Chat, $"{response[1].Url}");
                    try
                    {
                        await client.AnswerCallbackQueryAsync(ev.CallbackQuery.Id);
                    }
                    catch (Telegram.Bot.Exceptions.InvalidParameterException)
                    {
                        Console.WriteLine("Telegram.Bot.Exceptions.InvalidParameterException");
                        await client.SendTextMessageAsync(chatId: ev.CallbackQuery.Message.Chat, "There is something wrong, please repeat your request.", replyMarkup: GetButtons());
                    }
                }
            else if (ev.CallbackQuery.Data == "3M")
            {
                await client.SendTextMessageAsync(chatId: ev.CallbackQuery.Message.Chat, $"{response[2].Url}");
                    try
                    {
                        await client.AnswerCallbackQueryAsync(ev.CallbackQuery.Id);
                    }
                    catch (Telegram.Bot.Exceptions.InvalidParameterException)
                    {
                        Console.WriteLine("Telegram.Bot.Exceptions.InvalidParameterException");
                        await client.SendTextMessageAsync(chatId: ev.CallbackQuery.Message.Chat, "There is something wrong, please repeat your request.", replyMarkup: GetButtons());
                    }
                }
            else if (ev.CallbackQuery.Data == "4M")
            {
                await client.SendTextMessageAsync(chatId: ev.CallbackQuery.Message.Chat, $"{response[3].Url}");
                    try
                    {
                        await client.AnswerCallbackQueryAsync(ev.CallbackQuery.Id);
                    }
                    catch (Telegram.Bot.Exceptions.InvalidParameterException)
                    {
                        Console.WriteLine("Telegram.Bot.Exceptions.InvalidParameterException");
                        await client.SendTextMessageAsync(chatId: ev.CallbackQuery.Message.Chat, "There is something wrong, please repeat your request.", replyMarkup: GetButtons());
                    }
                }
            else if (ev.CallbackQuery.Data == "5M")
            {
                await client.SendTextMessageAsync(chatId: ev.CallbackQuery.Message.Chat, $"{response[4].Url}");
                    try
                    {
                        await client.AnswerCallbackQueryAsync(ev.CallbackQuery.Id);
                    }
                    catch (Telegram.Bot.Exceptions.InvalidParameterException)
                    {
                        Console.WriteLine("Telegram.Bot.Exceptions.InvalidParameterException");
                        await client.SendTextMessageAsync(chatId: ev.CallbackQuery.Message.Chat, "There is something wrong, please repeat your request.", replyMarkup: GetButtons());
                    }
                }
            else if (ev.CallbackQuery.Data == "6M")
            {
                await client.SendTextMessageAsync(chatId: ev.CallbackQuery.Message.Chat, $"{response[5].Url}");
                    try
                    {
                        await client.AnswerCallbackQueryAsync(ev.CallbackQuery.Id);
                    }
                    catch (Telegram.Bot.Exceptions.InvalidParameterException)
                    {
                        Console.WriteLine("Telegram.Bot.Exceptions.InvalidParameterException");
                        await client.SendTextMessageAsync(chatId: ev.CallbackQuery.Message.Chat, "There is something wrong, please repeat your request.", replyMarkup: GetButtons());
                    }
                }
            else if (ev.CallbackQuery.Data == "7M")
            {
                await client.SendTextMessageAsync(chatId: ev.CallbackQuery.Message.Chat, $"{response[6].Url}");
                    try
                    {
                        await client.AnswerCallbackQueryAsync(ev.CallbackQuery.Id);
                    }
                    catch (Telegram.Bot.Exceptions.InvalidParameterException)
                    {
                        Console.WriteLine("Telegram.Bot.Exceptions.InvalidParameterException");
                        await client.SendTextMessageAsync(chatId: ev.CallbackQuery.Message.Chat, "There is something wrong, please repeat your request.", replyMarkup: GetButtons());
                    }
                }
            else if (ev.CallbackQuery.Data == "8M")
            {
                await client.SendTextMessageAsync(chatId: ev.CallbackQuery.Message.Chat, $"{response[7].Url}");
                    try
                    {
                        await client.AnswerCallbackQueryAsync(ev.CallbackQuery.Id);
                    }
                    catch (Telegram.Bot.Exceptions.InvalidParameterException)
                    {
                        Console.WriteLine("Telegram.Bot.Exceptions.InvalidParameterException");
                        await client.SendTextMessageAsync(chatId: ev.CallbackQuery.Message.Chat, "There is something wrong, please repeat your request.", replyMarkup: GetButtons());
                    }
                }
            else if (ev.CallbackQuery.Data == "9M")
            {
                await client.SendTextMessageAsync(chatId: ev.CallbackQuery.Message.Chat, $"{response[8].Url}");
                    try
                    {
                        await client.AnswerCallbackQueryAsync(ev.CallbackQuery.Id);
                    }
                    catch (Telegram.Bot.Exceptions.InvalidParameterException)
                    {
                        Console.WriteLine("Telegram.Bot.Exceptions.InvalidParameterException");
                        await client.SendTextMessageAsync(chatId: ev.CallbackQuery.Message.Chat, "There is something wrong, please repeat your request.", replyMarkup: GetButtons());
                    }
                }
            else if (ev.CallbackQuery.Data == "10M")
            {
                await client.SendTextMessageAsync(chatId: ev.CallbackQuery.Message.Chat, $"{response[9].Url}");
                    try
                    {
                        await client.AnswerCallbackQueryAsync(ev.CallbackQuery.Id);
                    }
                    catch (Telegram.Bot.Exceptions.InvalidParameterException)
                    {
                        Console.WriteLine("Telegram.Bot.Exceptions.InvalidParameterException");
                        await client.SendTextMessageAsync(chatId: ev.CallbackQuery.Message.Chat, "There is something wrong, please repeat your request.", replyMarkup: GetButtons());
                    }
                }
            else if (ev.CallbackQuery.Data == "Exit")
            {
                await client.DeleteMessageAsync(chatId: ev.CallbackQuery.Message.Chat, ev.CallbackQuery.Message.MessageId);
            }
            }
            catch (System.Net.Http.HttpRequestException)
            {
                Console.WriteLine("System.Net.Http.HttpRequestException");
                await client.SendTextMessageAsync(chatId: ev.CallbackQuery.Message.Chat, "There is something wrong, please repeat your request.", replyMarkup: GetButtons());
            }
        }

        async static void ClientOnCallbackQuery1(object sender, CallbackQueryEventArgs ev)
        {
            try
            {
                string apiAddress = "https://webapplication120210531230754.azurewebsites.net/";

            var client3 = new HttpClient();
            client3.BaseAddress = new Uri(apiAddress);

            var result3 = await client3.GetAsync($"article/books");
            result3.EnsureSuccessStatusCode();
            var content3 = result3.Content.ReadAsStringAsync().Result;

            var response3 = JsonConvert.DeserializeObject<Response3[]>(content3);

            var message = ev.CallbackQuery.Message;
            if (ev.CallbackQuery.Data == "1B")
            {
                await client.SendPhotoAsync(
                            chatId: ev.CallbackQuery.Message.Chat,
                            photo: response3[0].book_image,
                            $"Title: { response3[0].title}\n" +
                    $"Author: {response3[0].author}\nDescription: {response3[0].description}");
                    try
                    {
                        await client.AnswerCallbackQueryAsync(ev.CallbackQuery.Id);
                    }
                    catch (Telegram.Bot.Exceptions.InvalidParameterException)
                    {
                        Console.WriteLine("Telegram.Bot.Exceptions.InvalidParameterException");
                        await client.SendTextMessageAsync(chatId: ev.CallbackQuery.Message.Chat, "There is something wrong, please repeat your request.", replyMarkup: GetButtons());
                    }
                }
            else if (ev.CallbackQuery.Data == "2B")
            {
                await client.SendPhotoAsync(
                            chatId: ev.CallbackQuery.Message.Chat,
                            photo: response3[1].book_image,
                            $"Title: { response3[1].title}\n" +
                    $"Author: {response3[1].author}\nDescription: {response3[1].description}");
                    try
                    {
                        await client.AnswerCallbackQueryAsync(ev.CallbackQuery.Id);
                    }
                    catch (Telegram.Bot.Exceptions.InvalidParameterException)
                    {
                        Console.WriteLine("Telegram.Bot.Exceptions.InvalidParameterException");
                        await client.SendTextMessageAsync(chatId: ev.CallbackQuery.Message.Chat, "There is something wrong, please repeat your request.", replyMarkup: GetButtons());
                    }
                }
            else if (ev.CallbackQuery.Data == "3B")
            {
                await client.SendPhotoAsync(
                            chatId: ev.CallbackQuery.Message.Chat,
                            photo: response3[2].book_image,
                            $"Title: { response3[2].title}\n" +
                    $"Author: {response3[2].author}\nDescription: {response3[2].description}");
                    try
                    {
                        await client.AnswerCallbackQueryAsync(ev.CallbackQuery.Id);
                    }
                    catch (Telegram.Bot.Exceptions.InvalidParameterException)
                    {
                        Console.WriteLine("Telegram.Bot.Exceptions.InvalidParameterException");
                        await client.SendTextMessageAsync(chatId: ev.CallbackQuery.Message.Chat, "There is something wrong, please repeat your request.", replyMarkup: GetButtons());
                    }
                }
            else if (ev.CallbackQuery.Data == "4B")
            {
                await client.SendPhotoAsync(
                            chatId: ev.CallbackQuery.Message.Chat,
                            photo: response3[3].book_image,
                            $"Title: { response3[3].title}\n" +
                    $"Author: {response3[3].author}\nDescription: {response3[3].description}");
                    try
                    {
                        await client.AnswerCallbackQueryAsync(ev.CallbackQuery.Id);
                    }
                    catch (Telegram.Bot.Exceptions.InvalidParameterException)
                    {
                        Console.WriteLine("Telegram.Bot.Exceptions.InvalidParameterException");
                        await client.SendTextMessageAsync(chatId: ev.CallbackQuery.Message.Chat, "There is something wrong, please repeat your request.", replyMarkup: GetButtons());
                    }
                }
            else if (ev.CallbackQuery.Data == "5B")
            {
                await client.SendPhotoAsync(
                            chatId: ev.CallbackQuery.Message.Chat,
                            photo: response3[4].book_image,
                            $"Title: { response3[4].title}\n" +
                    $"Author: {response3[4].author}\nDescription: {response3[4].description}");
                    try
                    {
                        await client.AnswerCallbackQueryAsync(ev.CallbackQuery.Id);
                    }
                    catch (Telegram.Bot.Exceptions.InvalidParameterException)
                    {
                        Console.WriteLine("Telegram.Bot.Exceptions.InvalidParameterException");
                        await client.SendTextMessageAsync(chatId: ev.CallbackQuery.Message.Chat, "There is something wrong, please repeat your request.", replyMarkup: GetButtons());
                    }
                }
            else if (ev.CallbackQuery.Data == "6B")
            {
                await client.SendPhotoAsync(
                            chatId: ev.CallbackQuery.Message.Chat,
                            photo: response3[5].book_image,
                            $"Title: { response3[5].title}\n" +
                    $"Author: {response3[5].author}\nDescription: {response3[5].description}");
                    try
                    {
                        await client.AnswerCallbackQueryAsync(ev.CallbackQuery.Id);
                    }
                    catch (Telegram.Bot.Exceptions.InvalidParameterException)
                    {
                        Console.WriteLine("Telegram.Bot.Exceptions.InvalidParameterException");
                        await client.SendTextMessageAsync(chatId: ev.CallbackQuery.Message.Chat, "There is something wrong, please repeat your request.", replyMarkup: GetButtons());
                    }
                }
            else if (ev.CallbackQuery.Data == "7B")
            {
                await client.SendPhotoAsync(
                            chatId: ev.CallbackQuery.Message.Chat,
                            photo: response3[6].book_image,
                            $"Title: { response3[6].title}\n" +
                    $"Author: {response3[6].author}\nDescription: {response3[6].description}");
                    try
                    {
                        await client.AnswerCallbackQueryAsync(ev.CallbackQuery.Id);
                    }
                    catch (Telegram.Bot.Exceptions.InvalidParameterException)
                    {
                        Console.WriteLine("Telegram.Bot.Exceptions.InvalidParameterException");
                        await client.SendTextMessageAsync(chatId: ev.CallbackQuery.Message.Chat, "There is something wrong, please repeat your request.", replyMarkup: GetButtons());
                    }
                }
            else if (ev.CallbackQuery.Data == "8B")
            {
                await client.SendPhotoAsync(
                            chatId: ev.CallbackQuery.Message.Chat,
                            photo: response3[7].book_image,
                            $"Title: { response3[7].title}\n" +
                    $"Author: {response3[7].author}\nDescription: {response3[7].description}");
                    try
                    {
                        await client.AnswerCallbackQueryAsync(ev.CallbackQuery.Id);
                    }
                    catch (Telegram.Bot.Exceptions.InvalidParameterException)
                    {
                        Console.WriteLine("Telegram.Bot.Exceptions.InvalidParameterException");
                        await client.SendTextMessageAsync(chatId: ev.CallbackQuery.Message.Chat, "There is something wrong, please repeat your request.", replyMarkup: GetButtons());
                    }
                }
            else if (ev.CallbackQuery.Data == "9B")
            {
                await client.SendPhotoAsync(
                            chatId: ev.CallbackQuery.Message.Chat,
                            photo: response3[8].book_image,
                            $"Title: { response3[8].title}\n" +
                    $"Author: {response3[8].author}\nDescription: {response3[8].description}");
                    try
                    {
                        await client.AnswerCallbackQueryAsync(ev.CallbackQuery.Id);
                    }
                    catch (Telegram.Bot.Exceptions.InvalidParameterException)
                    {
                        Console.WriteLine("Telegram.Bot.Exceptions.InvalidParameterException");
                        await client.SendTextMessageAsync(chatId: ev.CallbackQuery.Message.Chat, "There is something wrong, please repeat your request.", replyMarkup: GetButtons());
                    }
                }
            else if (ev.CallbackQuery.Data == "10B")
            {
                await client.SendPhotoAsync(
                            chatId: ev.CallbackQuery.Message.Chat,
                            photo: response3[9].book_image,
                            $"Title: { response3[9].title}\n" +
                    $"Author: {response3[9].author}\nDescription: {response3[9].description}");
                    try
                    {
                        await client.AnswerCallbackQueryAsync(ev.CallbackQuery.Id);
                    }
                    catch (Telegram.Bot.Exceptions.InvalidParameterException)
                    {
                        Console.WriteLine("Telegram.Bot.Exceptions.InvalidParameterException");
                        await client.SendTextMessageAsync(chatId: ev.CallbackQuery.Message.Chat, "There is something wrong, please repeat your request.", replyMarkup: GetButtons());
                    }
                }
            else if (ev.CallbackQuery.Data == "Exit1")
            {
                await client.DeleteMessageAsync(chatId: ev.CallbackQuery.Message.Chat, ev.CallbackQuery.Message.MessageId);
            }
            }
            catch (System.Net.Http.HttpRequestException)
            {
                Console.WriteLine("System.Net.Http.HttpRequestException");
                await client.SendTextMessageAsync(chatId: ev.CallbackQuery.Message.Chat, "There is something wrong, please repeat your request.", replyMarkup: GetButtons());
            }
        }

        public static InlineKeyboardMarkup NewOrderKeyboardFirst = new InlineKeyboardMarkup(new[]
{
    new []
    {
        InlineKeyboardButton.WithCallbackData("1 🖥","1M"),
        InlineKeyboardButton.WithCallbackData("2 💸", "2M"),
        InlineKeyboardButton.WithCallbackData("3 📱","3M"),
        InlineKeyboardButton.WithCallbackData("4 🖥", "4M"),
        InlineKeyboardButton.WithCallbackData("5 💸", "5M")
        
    },
    new []
    {
        InlineKeyboardButton.WithCallbackData("6 📱","6M"),
        InlineKeyboardButton.WithCallbackData("7 🖥", "7M"),
        InlineKeyboardButton.WithCallbackData("8 💸","8M"),
        InlineKeyboardButton.WithCallbackData("9 📱", "9M"),
        InlineKeyboardButton.WithCallbackData("10 🖥", "10M")
    },
    new []
    {
    InlineKeyboardButton.WithCallbackData("❌ Exit", "Exit")
    }
});

        public static InlineKeyboardMarkup NewOrderKeyboardFirst1 = new InlineKeyboardMarkup(new[]
{
    new []
    {
        InlineKeyboardButton.WithCallbackData("1 📗","1B"),
        InlineKeyboardButton.WithCallbackData("2 📙", "2B"),
        InlineKeyboardButton.WithCallbackData("3 📕","3B"),
        InlineKeyboardButton.WithCallbackData("4 📗", "4B"),
        InlineKeyboardButton.WithCallbackData("5📙", "5B")
    },
    new []
    {
        InlineKeyboardButton.WithCallbackData("6 📕","6B"),
        InlineKeyboardButton.WithCallbackData("7 📗", "7B"),
        InlineKeyboardButton.WithCallbackData("8 📙","8B"),
        InlineKeyboardButton.WithCallbackData("9 📕", "9B"),
        InlineKeyboardButton.WithCallbackData("10 📗", "10B")
    },
    new []
    {
    InlineKeyboardButton.WithCallbackData("❌ Exit", "Exit1")
    }
});
    }
}
