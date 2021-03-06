using System;
using System.Text;
using Telegram.Bot.Types;
using Telegram.Bot;
using System.Linq;

namespace NavalBattle
{
    /// <summary>
    /// Un "handler" del patrón Chain of Responsibility que implementa el comando "/rendirse".
    /// </summary>
    public class SurrenderHandler : BaseHandler
    {
        private GameUser user;

        private Match match;

        /// <summary>
        /// Constructor de SurrenderHandler.
        /// </summary>
        /// <param name="next">El próximo handler.</param>
        /// <returns></returns>
        public SurrenderHandler(BaseHandler next) : base(next)
        {
            this.Keywords = new string[] {"/rendirse"};
            this.user = null;
        }

        /// <summary>
        /// Procesa el mensaje "/rendirse", terminando la partida y dándole la victoria al otro jugador.
        /// </summary>
        /// <param name="message">El mensaje a procesar.</param>
        /// <param name="response">La respuesta al mensaje procesado.</param>
        /// <returns>true si el mensaje fue procesado; false en caso contrario.</returns>
        protected override bool InternalHandle(Message message, out string response)
        {
            try
            {
                if (this.CanHandle(message))
                {
                    this.user = UserRegister.Instance.GetUserByNickName(message.From.FirstName.ToString());

                    if (this.user.State != GameUser.UserState.InGame)
                    {
                        throw new InvalidStateException("No es posible realizar esta acción en este momento");
                    }

                    foreach (Match match in Admin.getAdmin().MatchList)
                    {
                        if (match.Players.Contains(this.user.Player))
                        {
                            this.match = match;
                        }
                    }

                    TelegramBotClient bot = ClientBot.GetBot();

                    long idPlayer0 = this.match.Players[0].ChatIdPlayer;

                    long idPlayer1 = this.match.Players[1].ChatIdPlayer;

                    bot.SendTextMessageAsync(idPlayer0, $"La partida finalizó. {this.user.NickName} se rindió");

                    bot.SendTextMessageAsync(idPlayer1, $"La partida finalizó. {this.user.NickName} se rindió");

                    Admin.getAdmin().MatchList.Remove(this.match);

                    GameUser user1 = UserRegister.Instance.GetUserById(idPlayer0);

                    GameUser user2 = UserRegister.Instance.GetUserById(idPlayer1);

                    user1.State = GameUser.UserState.NotInGame;

                    user2.State = GameUser.UserState.NotInGame;

                    response = string.Empty;

                    return true;
                }

                response = string.Empty;
                return false;
            }
            catch (NullReferenceException ne)
            {
                response = "Ingrese /start para acceder al menu de opciones.";

                return true;
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                this.Cancel();
                response = e.Message;

                return true;
            }
        }

        /// <summary>
        /// Retorna este "handler" al estado inicial. En los "handler" sin estado no hace nada. Los "handlers" que
        /// procesan varios mensajes cambiando de estado entre mensajes deben sobreescribir este método para volver al
        /// estado inicial.
        /// </summary>
        public override void Cancel()
        {
            this.InternalCancel();
            if (this.Next != null)
            {
                this.Next.Cancel();
            }
        }
    }
}