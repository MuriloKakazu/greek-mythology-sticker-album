using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using stickeralbum.Debug;
using stickeralbum.Game;
using stickeralbum.Generics;
using System;
using System.Linq;
using STDGEN = System.Collections.Generic;

namespace stickeralbum.Audio {
    public class SoundPlayer : IDisposable {
        private static LinkedList<SoundPlayer> Instances;
        private WaveOutEvent Device;
        private MixingSampleProvider Mixer;

        public static SoundPlayer Instance 
            => Instances.Add(new SoundPlayer(44100, 2));

        public PlaybackState State { get; set; }

        public Single Volume {
            get => Device.Volume;
            set => GameMaster.Settings.Volume = Device.Volume = value;
        }
        public Boolean Loop { get; protected set; }
        public SoundTrack Track { get; protected set; }

        static SoundPlayer() {
            Instances = new LinkedList<SoundPlayer>();
        }

        public SoundPlayer(Int32 sampleRate = 44100, Int32 channelCount = 2) {
            Device = new WaveOutEvent();
            Mixer  = new MixingSampleProvider(
                WaveFormat.CreateIeeeFloatWaveFormat(sampleRate, channelCount)
            );
            Mixer.ReadFully = true;
            Device.Init(Mixer);
            Device.PlaybackStopped += Device_PlaybackStopped;
            Mixer.MixerInputEnded += Mixer_MixerInputEnded;
        }

        private void Mixer_MixerInputEnded(object sender, SampleProviderEventArgs e) {
            State = PlaybackState.Stopped;
            DebugUtils.LogAudio($"Input for <{Track.ID}> stopped.");
            if (Loop) {
                Instance.Play(Track, Loop);
            }
        }

        private void Device_PlaybackStopped(object sender, StoppedEventArgs e) {
            State = PlaybackState.Stopped;
            if (e?.Exception != null) {
                //DebugUtils.LogError($"Playback for <{Track?.ID}> stopped with an Exception => {e?.Exception?.Message}");
            } else {
                DebugUtils.LogAudio($"Playback for <{Track?.ID}> stopped.");
            }

            if (!Loop) {
                Dispose();
            }
        }

        private void AddMixerInput(ISampleProvider input) 
            => Mixer.AddMixerInput(ConvertToRightChannelCount(input));

        private ISampleProvider ConvertToRightChannelCount(ISampleProvider input) {
            var inputWfCh = input.WaveFormat.Channels;
            var mixerWfCh = Mixer.WaveFormat.Channels;
            if (inputWfCh == mixerWfCh) {
                return input;
            }
            if (inputWfCh == 1 && mixerWfCh == 2) {
                return new MonoToStereoSampleProvider(input);
            }
            return null;
        }

        public void Stop() {
            Loop = false;
            //Device_PlaybackStopped(null, null);
            Device.Stop();
        }

        public void Play(SoundTrack track, Boolean loop = false) {
            if (track == null || Device == null || Mixer == null) {
                //DebugUtils.LogError($"Error playing track => <{track?.ID}>. Reason => Null reference in SoundPlayer");
                return;
            }
            try {
                StopAll(track.ID);
                //track.Setup();
                Track  = track;
                Loop   = loop;
                Volume = GameMaster.Settings.Volume;
                State  = PlaybackState.Playing;
                AddMixerInput(new SoundSampleProvider(track));
                Device.Play();
                DebugUtils.LogAudio($"Playing track => <{track?.ID}> Volume => {Volume * 100f}%");
            } catch (Exception e) {
                //DebugUtils.LogError($"Error playing track => <{track?.ID}>. Reason => {e.Message}");
            }
        }

        public static void StopAll()
            => Instances.ForEach(x => {
               x.Stop();
            });

        public static void StopAll(String id)
            => Instances.ForEach(x => {
               if (x?.Track?.ID == id) {
                   x.Stop();
               }
            });

        public static STDGEN.IEnumerable<SoundPlayer> 
            GetInstances() => Instances;

        public static STDGEN.IEnumerable<SoundTrack> AllTracksPlaying()
            => from x in Instances where x?.State == PlaybackState.Playing select x?.Track;

        public void Dispose() {
            Track = null;
            Mixer.RemoveAllMixerInputs();
            Instances.Remove(this);
            Device.Dispose();
        }
    }
}
