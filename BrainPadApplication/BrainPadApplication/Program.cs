using System;
using System.Collections;

namespace BrainPadApplication
{
    public class Program
    {
        private int _pass;
        private int _level = 1;
        private int _brainDirection;
        private int _accelerometerDirection;
        public bool BrainNeedsToPickDirection;
        public bool GameHasStarted;
        public bool FullLights;
        public bool SoundOn;
        private readonly ArrayList _inputPattern = new ArrayList();
        private readonly ArrayList _brainPattern = new ArrayList();
        private Array _ipa;
        private Array _pa;

        public void BrainPadSetup()
        {
            BrainPad.Display.DrawSmallTextAndShowOnScreen(1, 0, "Welcome to Brain Says\r\nPress Down for\r\nDirections");
            BrainNeedsToPickDirection = true;
        }

        public void BrainPadLoop()
        {
            if (GameHasStarted)
                if (BrainNeedsToPickDirection)
                    ChooseADirection();
                else
                    ReadAccelerometer();

            if (BrainPad.Buttons.IsUpPressed())
                GameHasStarted = true;
            if (BrainPad.Buttons.IsDownPressed())
            {
                BrainPad.Display.DrawSmallTextAndShowOnScreen(1, 0,
                    "Brain will tell you\r\nwhich way to tilt.\r\nPress Up to start\r\nBrain Says\r\n\nPress Right\r\nFor Full Lights");
                BrainPad.Wait.Seconds(1.0);
                BrainPad.Display.DrawSmallTextAndShowOnScreen(1, 0,
                    "Brain will tell you\r\nwhich way to tilt.\r\nPress Up to start\r\nBrain Says\r\n\nPress Left\r\nTo Enable Sound");
                BrainPad.Wait.Seconds(1.0);
                BrainPad.Display.DrawSmallTextAndShowOnScreen(1, 0,
                    "Brain will tell you\r\nwhich way to tilt.\r\nPress Up to start\r\nBrain Says\r\n\nPress Down\r\nTo Repeat");
            }
            if (BrainPad.Buttons.IsLeftPressed())
                SoundOn = true;
            if (BrainPad.Buttons.IsRightPressed())
            {
                FullLights = true;
                BrainPad.LightBulb.TurnOff();
            }
        }

        public void ChooseADirection()
        {
            BrainPad.Display.DrawTextAndShowOnScreen(1, 0, "Brain Says");
            BrainPad.Wait.Seconds(1);
            var r = new Random();
            _brainDirection = r.Next(4) + 1;

            _brainPattern.Add(_brainDirection);

            foreach (int brainDirection in _brainPattern)
            {
                switch (brainDirection)
                {
                    case 1:
                        BrainPad.Display.DrawTextAndShowOnScreen(1, 0, "   Tilt\r\n   Left");
                        if (FullLights)
                            BrainPad.LightBulb.TurnColor(100, 100, 0);
                        else
                            BrainPad.LightBulb.TurnColor(30, 30, 0);
                        if (SoundOn)
                        {
                            BrainPad.Buzzer.StartBuzzing(85);
                            BrainPad.Wait.Seconds(.15);
                            BrainPad.Buzzer.StopBuzzing();
                        }
                        BrainPad.Wait.Seconds(1.5);
                        BrainPad.LightBulb.TurnOff();
                        break;

                    case 2:
                        BrainPad.Display.DrawTextAndShowOnScreen(1, 0, "   Tilt\r\n  Forward");
                        if (FullLights)
                            BrainPad.LightBulb.TurnColor(0, 100, 0);
                        else
                            BrainPad.LightBulb.TurnColor(0, 30, 0);
                        if (SoundOn)
                        {
                            BrainPad.Buzzer.StartBuzzing(130);
                            BrainPad.Wait.Seconds(.15);
                            BrainPad.Buzzer.StopBuzzing();
                        }
                        BrainPad.Wait.Seconds(1.5);
                        BrainPad.LightBulb.TurnOff();
                        break;

                    case 3:
                        BrainPad.Display.DrawTextAndShowOnScreen(1, 0, "   Tilt\r\n Backwards");
                        if (FullLights)
                            BrainPad.LightBulb.TurnColor(0, 0, 100);
                        else
                            BrainPad.LightBulb.TurnColor(0, 0, 30);
                        if (SoundOn)
                        {
                            BrainPad.Buzzer.StartBuzzing(40);
                            BrainPad.Wait.Seconds(.15);
                            BrainPad.Buzzer.StopBuzzing();
                        }
                        BrainPad.Wait.Seconds(1.5);
                        BrainPad.LightBulb.TurnOff();
                        break;

                    case 4:
                        BrainPad.Display.DrawTextAndShowOnScreen(1, 0, "   Tilt\r\n   Right");
                        if (FullLights)
                            BrainPad.LightBulb.TurnColor(100, 0, 0);
                        else
                            BrainPad.LightBulb.TurnColor(30, 0, 0);
                        if (SoundOn)
                        {
                            BrainPad.Buzzer.StartBuzzing(180);
                            BrainPad.Wait.Seconds(.15);
                            BrainPad.Buzzer.StopBuzzing();
                        }
                        BrainPad.Wait.Seconds(1.5);
                        BrainPad.LightBulb.TurnOff();
                        break;
                }
                BrainNeedsToPickDirection = false;
            }

            BrainPad.LightBulb.TurnOff();
            BrainPad.Display.DrawTextAndShowOnScreen(1, 0, "Now It's\r\nYour Turn");
            BrainPad.Wait.Seconds(2);
        }

        private void ReadAccelerometer()
        {
            var accelX = BrainPad.Accelerometer.ReadX() * 1000;
            var accelY = BrainPad.Accelerometer.ReadY() * 1000;

            if (accelX < -450)
            {
                BrainPad.Display.DrawTextAndShowOnScreen(1, 0, "Left");
                _accelerometerDirection = 1;
                if (FullLights)
                    BrainPad.LightBulb.TurnColor(100, 100, 0);
                else
                    BrainPad.LightBulb.TurnColor(30, 30, 0);
                if (SoundOn)
                {
                    BrainPad.Buzzer.StartBuzzing(85);
                    BrainPad.Wait.Seconds(.15);
                    BrainPad.Buzzer.StopBuzzing();
                }
                _inputPattern.Add(_accelerometerDirection);
                BrainPad.Wait.Seconds(1);
                BrainPad.LightBulb.TurnOff();
                if (_inputPattern.Count == _brainPattern.Count)
                    Comparebrainanduserpattern();
            }

            if (accelY > 450)
            {
                BrainPad.Display.DrawTextAndShowOnScreen(1, 0, "Forward");
                _accelerometerDirection = 2;
                if (FullLights)
                    BrainPad.LightBulb.TurnColor(0, 100, 0);
                else
                    BrainPad.LightBulb.TurnColor(0, 30, 0);
                if (SoundOn)
                {
                    BrainPad.Buzzer.StartBuzzing(130);
                    BrainPad.Wait.Seconds(.15);
                    BrainPad.Buzzer.StopBuzzing();
                }
                _inputPattern.Add(_accelerometerDirection);
                BrainPad.Wait.Seconds(1);
                BrainPad.LightBulb.TurnOff();
                if (_inputPattern.Count == _brainPattern.Count)
                    Comparebrainanduserpattern();
            }

            if (accelY < -450)
            {
                BrainPad.Display.DrawTextAndShowOnScreen(1, 0, "Backwards");
                _accelerometerDirection = 3;
                if (FullLights)
                    BrainPad.LightBulb.TurnColor(0, 0, 100);
                else
                    BrainPad.LightBulb.TurnColor(0, 0, 30);
                if (SoundOn)
                {
                    BrainPad.Buzzer.StartBuzzing(40);
                    BrainPad.Wait.Seconds(.15);
                    BrainPad.Buzzer.StopBuzzing();
                }
                _inputPattern.Add(_accelerometerDirection);
                BrainPad.Wait.Seconds(1);
                BrainPad.LightBulb.TurnOff();
                if (_inputPattern.Count == _brainPattern.Count)
                    Comparebrainanduserpattern();
            }

            if (accelX > 450)
            {
                BrainPad.Display.DrawTextAndShowOnScreen(1, 0, "Right");
                _accelerometerDirection = 4;
                if (FullLights)
                    BrainPad.LightBulb.TurnColor(100, 0, 0);
                else
                    BrainPad.LightBulb.TurnColor(30, 0, 0);
                if (SoundOn)
                {
                    BrainPad.Buzzer.StartBuzzing(180);
                    BrainPad.Wait.Seconds(.15);
                    BrainPad.Buzzer.StopBuzzing();
                }
                _inputPattern.Add(_accelerometerDirection);
                BrainPad.Wait.Seconds(1);
                BrainPad.LightBulb.TurnOff();
                if (_inputPattern.Count == _brainPattern.Count)
                    Comparebrainanduserpattern();
            }

            void Comparebrainanduserpattern()
            {
                _pa = _brainPattern.ToArray();
                _ipa = _inputPattern.ToArray();
                foreach (int brainDirection in _pa)
                foreach (int accelerometerDirection in _ipa)
                    if (brainDirection == accelerometerDirection)
                        _pass = 1;
                    else
                        _pass = 0;

                if (_pass == 1)
                {
                    BrainPad.LightBulb.TurnOff();
                    _inputPattern.Clear();
                    UserLevel();
                }
                else
                {
                    _brainPattern.Clear();
                    _inputPattern.Clear();
                    GameOver();
                }
            }
        }

        private void UserLevel()
        {
            if (_accelerometerDirection == _brainDirection)
            {
                BrainPad.Display.DrawTextAndShowOnScreen(1, 0, "You Beat\r\nLevel " + _level);
                BrainPad.Wait.Seconds(1);
                _level++;
                BrainNeedsToPickDirection = true;
            }
        }

        private void GameOver()
        {
            BrainPad.LightBulb.TurnOff();
            BrainPad.Display.DrawSmallTextAndShowOnScreen(1, 0,
                "Game Over.  Good Try!\r\nYou Made It To\r\nLevel " + _level);
            BrainPad.Buzzer.StartBuzzing(50);
            BrainPad.Wait.Seconds(1.5);
            BrainPad.Buzzer.StopBuzzing();
            GameHasStarted = false;
            BrainNeedsToPickDirection = true;
        }
    }
}