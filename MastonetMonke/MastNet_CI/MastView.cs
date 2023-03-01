using ComputerInterface;
using ComputerInterface.ViewLib;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MastonetMonke.MastNet_CI
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

        // This is called when you view is opened
        public override void OnShow(object[] args)
        {
            base.OnShow(args);

            UpdateMonitor();
        }

        public void UpdateMonitor()
        {
            var str = new StringBuilder();

            str.AppendLine("MastonetMonke").AppendLines(2); // Header

            str.BeginColor("ffffff50") // Beginning text
                .Append("Text: ")
                .EndColor();

            str.BeginColor(_hasPosted ? "6CD87Aff" : "ffffff50") // Input
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

            string orgMessage = _textInputHandler.Text.ToLower();
            List<string> orgMessage_Split = orgMessage.Split(' ').ToList();
            orgMessage_Split[0] = string.Concat(char.ToUpper(orgMessage_Split[0][0]).ToString(), orgMessage_Split[0].Substring(1));
            string finalMessage = string.Join(" ", orgMessage_Split);

            _textInputHandler.Text = finalMessage;
            Plugin.Instance.MastManager.PublicMastodonClient.PublishStatus(status: finalMessage, visibility: Mastonet.Visibility.Public);
        }
    }
}