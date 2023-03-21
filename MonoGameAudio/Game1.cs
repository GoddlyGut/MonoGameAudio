using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System.Linq;
using System.Xml.Linq;
using System.Diagnostics;

public class Question
{
    public string question;
    public string choice1;
    public string choice2;
    public string choice3;
    public string choice4;
    public PossibleChoices correctChoice;

    public enum PossibleChoices
    {
        X, 
        Y,
        A,
        B,
    }

    

    public Question(string question, string choice1, string choice2, string choice3, string choice4, PossibleChoices correctChoice)
    {
        this.question = question;
        this.choice1 = choice1;
        this.choice2 = choice2;
        this.choice3 = choice3;
        this.choice4 = choice4;
        this.correctChoice = correctChoice;
        
    }
}

namespace MonoGameAudio
{

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Song song1;
        SoundEffect ding;
        GamePadState pad, oldPad;
        KeyboardState keyboard;
        SpriteFont font;
        bool pressingKey;

        int currentQuestion = 0;
        bool questionGenerated = false;

        Question[] questions = new Question[]
        {
            new Question("test", "test", "test", "test", "test", Question.PossibleChoices.X),
            new Question("test", "test", "test", "test", "test", Question.PossibleChoices.Y),
            new Question("test", "test", "test", "test", "test", Question.PossibleChoices.A),
            new Question("test", "test", "test", "test", "test", Question.PossibleChoices.B),
        };
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            ding = Content.Load<SoundEffect>("ding");
            //song1 = Content.Load<Song>("03 YYZ - Rush");
            font = Content.Load<SpriteFont>("OnScreen");
        }

        protected override void Update(GameTime gameTime)
        {

            pad = GamePad.GetState(PlayerIndex.One);
            keyboard = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (pad.Buttons.RightShoulder == ButtonState.Pressed && oldPad.Buttons.RightShoulder == ButtonState.Released)
            {
                ding.Play();
                /*if (MediaPlayer.State == MediaState.Stopped)
                {
                    MediaPlayer.Play(song1);
                }
                else if (MediaPlayer.State == MediaState.Playing)
                {
                    MediaPlayer.Pause();
                }
                else if (MediaPlayer.State == MediaState.Paused) 
                {
                    MediaPlayer.Resume();
                }*/
            }

            Question.PossibleChoices correctChoice = questions[currentQuestion].correctChoice;

            if ((pad.IsButtonDown(Buttons.X) || keyboard.IsKeyDown(Keys.X)) && correctChoice == Question.PossibleChoices.X)
            {
                pressingKey = true;
                NextQuestion();
            }
            else if ((pad.IsButtonDown(Buttons.Y) || keyboard.IsKeyDown(Keys.Y)) && correctChoice == Question.PossibleChoices.Y)
            {
                pressingKey = true;
                NextQuestion();
            }
            else if ((pad.IsButtonDown(Buttons.A) || keyboard.IsKeyDown(Keys.A)) && correctChoice == Question.PossibleChoices.A)
            {
                pressingKey = true;
                NextQuestion();
            }
            else if ((pad.IsButtonDown(Buttons.B) || keyboard.IsKeyDown(Keys.B)) && correctChoice == Question.PossibleChoices.B)
            {
                pressingKey = true;
                NextQuestion();
            }

            if (keyboard.IsKeyUp(Keys.X)) { pressingKey = false; }
            else if (keyboard.IsKeyUp(Keys.Y)) { pressingKey = false; }
            else if (keyboard.IsKeyUp(Keys.A)) { pressingKey = false; }
            else if (keyboard.IsKeyUp(Keys.B)) { pressingKey = false; }

            oldPad = pad;



            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            // TODO: Add your drawing code here

            displayQuestion(currentQuestion);


           

            _spriteBatch.End();


            base.Draw(gameTime);
        }

        public void displayQuestion(int questionNum)
        {
            
            _spriteBatch.DrawString(font, "Song Name Here", new Vector2(350, 250), Color.White);

            for (int i = 0; i < questions.Count(); ++i)
            {
                if (i == currentQuestion)
                {
                    _spriteBatch.DrawString(font, " #" + (currentQuestion + 1).ToString() + ". " + questions[i].question, new Vector2(0, 0), Color.White);
                    _spriteBatch.DrawString(font, " X. " + questions[i].choice1, new Vector2(0, 30), Color.White);
                    _spriteBatch.DrawString(font, " Y. " + questions[i].choice2, new Vector2(0, 50), Color.White);
                    _spriteBatch.DrawString(font, " A. " + questions[i].choice3, new Vector2(0, 70), Color.White);
                    _spriteBatch.DrawString(font, " B. " + questions[i].choice4, new Vector2(0, 90), Color.White);
                }
            }

        }

        public void NextQuestion()
        {
            Debug.WriteLine("Detected input");
            if (currentQuestion < questions.Length)
            {
                currentQuestion++;
            }
        }

        
    }
}