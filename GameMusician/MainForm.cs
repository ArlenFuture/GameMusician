using GameMusician.Constants;
using InputManager;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Multimedia;
using Melanchall.DryWetMidi.MusicTheory;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameMusician
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        string _filePath = string.Empty;
        private static Playback _playback;

        #region FormLoad & FormClosed
        private void MainForm_Load(object sender, EventArgs e)
        {
            RegisterHotKey(this.Handle, Hotkey.ID4Start_Int, 2, Keys.F5);
        }
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            UnregisterHotKey(this.Handle, Hotkey.ID4Start_Int);
            _playback?.Dispose();
        }
        #endregion
        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "midi files (*.mid)|*.mid";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    _filePath = openFileDialog.FileName;
                    selectedSong_Label.Text = Path.GetFileName(_filePath);
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(About.Text, About.Caption);
        }

        #region Hotkey
        [DllImport(DllImport.user32)]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, uint control, Keys vk);
        [DllImport(DllImport.user32)]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x0312:
                    if (m.WParam.ToString().Equals(Hotkey.ID4Start_Str))
                    {
                        if (_playback == null)
                        {
                            midiPlayerStart();
                        }
                        else
                        {
                            if (!_playback.IsRunning)
                            {
                                midiPlayerStart();
                            }
                            else
                            {
                                midiPlayerStop();
                            }
                        }
                    }
                    break;
            }
            base.WndProc(ref m);
        }
        #endregion

        #region Midi Player
        private void midiPlayerStart()
        {
            if (_filePath != string.Empty)
            {
                var midiFile = MidiFile.Read(_filePath);
                _playback = midiFile.GetPlayback();
                _playback.NotesPlaybackStarted += _playback_OnNotesPlaybackStarted;
                _playback.NotesPlaybackFinished += _playback_NotesPlaybackFinished;
                _playback.Start();

            }
            else
            {
                MessageBox.Show("Please select file.", "Please select file.");
            }
        }
        private void midiPlayerStop()
        {
            _playback.Stop();
        }
        private static void _playback_OnNotesPlaybackStarted(object? sender, NotesEventArgs e)
        {
            var nowNote = e.Notes.FirstOrDefault();
            switch (nowNote.Octave)
            {
                //高音
                case > 4:
                    switch (nowNote.NoteName)
                    {
                        case Melanchall.DryWetMidi.MusicTheory.NoteName.C:
                            Keyboard.KeyDown(Keys.Q);
                            break;
                        case Melanchall.DryWetMidi.MusicTheory.NoteName.D:
                            Keyboard.KeyDown(Keys.W);
                            break;
                        case Melanchall.DryWetMidi.MusicTheory.NoteName.E:
                            Keyboard.KeyDown(Keys.E);
                            break;
                        case Melanchall.DryWetMidi.MusicTheory.NoteName.F:
                            Keyboard.KeyDown(Keys.R);
                            break;
                        case Melanchall.DryWetMidi.MusicTheory.NoteName.G:
                            Keyboard.KeyDown(Keys.T);
                            break;
                        case Melanchall.DryWetMidi.MusicTheory.NoteName.A:
                            Keyboard.KeyDown(Keys.Y);
                            break;
                        case Melanchall.DryWetMidi.MusicTheory.NoteName.B:
                            Keyboard.KeyDown(Keys.U);
                            break;
                        default:
                            break;
                    }
                    break;
                //低音
                case < 4:
                    switch (nowNote.NoteName)
                    {
                        case Melanchall.DryWetMidi.MusicTheory.NoteName.C:
                            Keyboard.KeyDown(Keys.Z);
                            break;
                        case Melanchall.DryWetMidi.MusicTheory.NoteName.D:
                            Keyboard.KeyDown(Keys.X);
                            break;
                        case Melanchall.DryWetMidi.MusicTheory.NoteName.E:
                            Keyboard.KeyDown(Keys.C);
                            break;
                        case Melanchall.DryWetMidi.MusicTheory.NoteName.F:
                            Keyboard.KeyDown(Keys.V);
                            break;
                        case Melanchall.DryWetMidi.MusicTheory.NoteName.G:
                            Keyboard.KeyDown(Keys.B);
                            break;
                        case Melanchall.DryWetMidi.MusicTheory.NoteName.A:
                            Keyboard.KeyDown(Keys.N);
                            break;
                        case Melanchall.DryWetMidi.MusicTheory.NoteName.B:
                            Keyboard.KeyDown(Keys.M);
                            break;
                        default:
                            break;
                    }
                    break;
                case 4:
                    switch (nowNote.NoteName)
                    {
                        case Melanchall.DryWetMidi.MusicTheory.NoteName.C:
                            Keyboard.KeyDown(Keys.A);
                            break;
                        case Melanchall.DryWetMidi.MusicTheory.NoteName.D:
                            Keyboard.KeyDown(Keys.S);
                            break;
                        case Melanchall.DryWetMidi.MusicTheory.NoteName.E:
                            Keyboard.KeyDown(Keys.D);
                            break;
                        case Melanchall.DryWetMidi.MusicTheory.NoteName.F:
                            Keyboard.KeyDown(Keys.F);
                            break;
                        case Melanchall.DryWetMidi.MusicTheory.NoteName.G:
                            Keyboard.KeyDown(Keys.G);
                            break;
                        case Melanchall.DryWetMidi.MusicTheory.NoteName.A:
                            Keyboard.KeyDown(Keys.H);
                            break;
                        case Melanchall.DryWetMidi.MusicTheory.NoteName.B:
                            Keyboard.KeyDown(Keys.J);
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }

        }
        private void _playback_NotesPlaybackFinished(object? sender, NotesEventArgs e)
        {
            var nowNote = e.Notes.FirstOrDefault();
            switch (nowNote.Octave)
            {
                //高音
                case > 4:
                    switch (nowNote.NoteName)
                    {
                        case Melanchall.DryWetMidi.MusicTheory.NoteName.C:
                            Keyboard.KeyUp(Keys.Q);
                            break;
                        case Melanchall.DryWetMidi.MusicTheory.NoteName.D:
                            Keyboard.KeyUp(Keys.W);
                            break;
                        case Melanchall.DryWetMidi.MusicTheory.NoteName.E:
                            Keyboard.KeyUp(Keys.E);
                            break;
                        case Melanchall.DryWetMidi.MusicTheory.NoteName.F:
                            Keyboard.KeyUp(Keys.R);
                            break;
                        case Melanchall.DryWetMidi.MusicTheory.NoteName.G:
                            Keyboard.KeyUp(Keys.T);
                            break;
                        case Melanchall.DryWetMidi.MusicTheory.NoteName.A:
                            Keyboard.KeyUp(Keys.Y);
                            break;
                        case Melanchall.DryWetMidi.MusicTheory.NoteName.B:
                            Keyboard.KeyUp(Keys.U);
                            break;
                        default:
                            break;
                    }
                    break;
                //低音
                case < 4:
                    switch (nowNote.NoteName)
                    {
                        case Melanchall.DryWetMidi.MusicTheory.NoteName.C:
                            Keyboard.KeyUp(Keys.Z);
                            break;
                        case Melanchall.DryWetMidi.MusicTheory.NoteName.D:
                            Keyboard.KeyUp(Keys.X);
                            break;
                        case Melanchall.DryWetMidi.MusicTheory.NoteName.E:
                            Keyboard.KeyUp(Keys.C);
                            break;
                        case Melanchall.DryWetMidi.MusicTheory.NoteName.F:
                            Keyboard.KeyUp(Keys.V);
                            break;
                        case Melanchall.DryWetMidi.MusicTheory.NoteName.G:
                            Keyboard.KeyUp(Keys.B);
                            break;
                        case Melanchall.DryWetMidi.MusicTheory.NoteName.A:
                            Keyboard.KeyUp(Keys.N);
                            break;
                        case Melanchall.DryWetMidi.MusicTheory.NoteName.B:
                            Keyboard.KeyUp(Keys.M);
                            break;
                        default:
                            break;
                    }
                    break;
                case 4:
                    switch (nowNote.NoteName)
                    {
                        case Melanchall.DryWetMidi.MusicTheory.NoteName.C:
                            Keyboard.KeyUp(Keys.A);
                            break;
                        case Melanchall.DryWetMidi.MusicTheory.NoteName.D:
                            Keyboard.KeyUp(Keys.S);
                            break;
                        case Melanchall.DryWetMidi.MusicTheory.NoteName.E:
                            Keyboard.KeyUp(Keys.D);
                            break;
                        case Melanchall.DryWetMidi.MusicTheory.NoteName.F:
                            Keyboard.KeyUp(Keys.F);
                            break;
                        case Melanchall.DryWetMidi.MusicTheory.NoteName.G:
                            Keyboard.KeyUp(Keys.G);
                            break;
                        case Melanchall.DryWetMidi.MusicTheory.NoteName.A:
                            Keyboard.KeyUp(Keys.H);
                            break;
                        case Melanchall.DryWetMidi.MusicTheory.NoteName.B:
                            Keyboard.KeyUp(Keys.J);
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
        }
        #endregion


    }
}
