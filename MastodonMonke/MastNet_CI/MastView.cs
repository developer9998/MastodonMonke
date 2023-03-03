using BepInEx;
using ComputerInterface;
using ComputerInterface.ViewLib;
using MastodonMonke;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MastodonMonke.MastNet_CI
{
    public class MastView : ComputerView
    {
        private readonly UITextInputHandler _textInputHandler;
        private bool _hasPosted;

        public MastView()
        {
            _hasPosted = false;
            _textInputHandler = new UITextInputHandler();
        }

        public override void OnShow(object[] args)
        {
            base.OnShow(args);

            UpdateMonitor();
        }

        public void UpdateMonitor()
        {
            var str = new StringBuilder();

            str.Repeat("=", SCREEN_WIDTH) // Header
                .BeginCenter()
                .AppendLine();

            str.AppendLine(PluginInfo.Name)
                .AppendLine("Made by Dev & WowItsKaylie")
                .EndAlign();

            str.Repeat("=", SCREEN_WIDTH)
                .AppendLines(2);

            str.BeginColor("ffffff50") // Beginning text
                .Append("Text: ")
                .EndColor();

            str.BeginColor(_hasPosted ? "6CD87Aff" : "ffffffff") // Input
                .Append(_textInputHandler.Text)
                .EndColor()
                .AppendClr("_", "ffffff50");

            SetText(str);
        }

        public override void OnKeyPressed(EKeyboardKey key)
        {
            switch (key)
            {
                case EKeyboardKey.Enter:
                    PostMessage();
                    UpdateMonitor();
                    return;
                case EKeyboardKey.Back:
                    ReturnToMainMenu();
                    _textInputHandler.Text = "";
                    return;
            }

            if (_textInputHandler.HandleKey(key))
            {
                _hasPosted = false;

                int len = SCREEN_WIDTH * SCREEN_HEIGHT - 4;
                if (_textInputHandler.Text.Length > len) _textInputHandler.Text = _textInputHandler.Text.Substring(0, len);

                UpdateMonitor();
            }
        }

        public void PostMessage()
        {
            _hasPosted = true;

            if (_textInputHandler.Text.IsNullOrWhiteSpace()) return;

            string orgMessage = _textInputHandler.Text.ToLower();
            List<string> orgMessage_Split = orgMessage.Split(' ').ToList();
            orgMessage_Split[0] = string.Concat(char.ToUpper(orgMessage_Split[0][0]).ToString(), orgMessage_Split[0].Substring(1));
            string finalMessage = string.Join(" ", orgMessage_Split);

            _textInputHandler.Text = finalMessage;
            Plugin.Instance.MastManager.PublicMastodonClient.PublishStatus(status: finalMessage, visibility: Mastonet.Visibility.Public);
        }
    }
}