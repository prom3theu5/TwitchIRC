using System;
using System.Collections.Generic;

namespace TwitchLib.TwitchIRC
{
    public enum ChannelModeChangeAction {
        Set,
        Unset
    }

    public enum ChannelMode {
        Unknown,
        Op = 'o',
        Owner = 'q',
        Admin = 'a',
        HalfOp = 'h',
        Voice = 'v',
        Ban = 'b',
        BanException = 'e',
        InviteException = 'I',
        Key = 'k',
        UserLimit = 'l',
        TopicLock = 't'
    }

    public enum ChannelModeHasParameter {
        Always,
        OnlySet,
        Never
    }

    public class ChannelModeInfo
    {
        public ChannelMode Mode { get; set; }
        public ChannelModeHasParameter HasParameter { get; set; }

        public ChannelModeInfo(ChannelMode mode, ChannelModeHasParameter hasParameter)
        {
            Mode = mode;
            HasParameter = hasParameter;
        }
    }

    public class ChannelModeMap : Dictionary<char, ChannelModeInfo>
    {
        // TODO: verify RFC modes!
        public ChannelModeMap() :
            // Smuxi mapping
            this("oqahvbeI,k,l,imnpstr")
            // IRCnet mapping
            //this("beIR,k,l,imnpstaqr")
        {
        }

        public ChannelModeMap(string channelModes)
        {
            Parse(channelModes);
        }

        public void Parse(string channelModes)
        {
            var listAlways = channelModes.Split(',')[0];
            var settingAlways = channelModes.Split(',')[1];
            var onlySet = channelModes.Split(',')[2];
            var never = channelModes.Split(',')[3];

            foreach (var mode in listAlways) {
                this[mode] = new ChannelModeInfo((ChannelMode) mode, ChannelModeHasParameter.Always);
            }
            foreach (var mode in settingAlways) {
                this[mode] = new ChannelModeInfo((ChannelMode) mode, ChannelModeHasParameter.Always);
            }
            foreach (var mode in onlySet) {
                this[mode] = new ChannelModeInfo((ChannelMode) mode, ChannelModeHasParameter.OnlySet);
            }
            foreach (var mode in never) {
                this[mode] = new ChannelModeInfo((ChannelMode) mode, ChannelModeHasParameter.Never);
            }
        }
    }

    public class ChannelModeChangeInfo
    {
        public ChannelModeChangeAction Action { get; private set; }
        public ChannelMode Mode { get; private set; }
        public char ModeChar { get; private set; }
        public string Parameter { get; private set; }

        public ChannelModeChangeInfo()
        {
        }

        public static List<ChannelModeChangeInfo> Parse(ChannelModeMap modeMap, string target, string mode, string modeParameters)
        {
            if (modeMap == null) {
                throw new ArgumentNullException("modeMap");
            }
            if (target == null) {
                throw new ArgumentNullException("target");
            }
            if (mode == null) {
                throw new ArgumentNullException("mode");
            }
            if (modeParameters == null) {
                throw new ArgumentNullException("modeParameters");
            }

            var modeChanges = new List<ChannelModeChangeInfo>();

            var action = ChannelModeChangeAction.Set;
            var parameters = modeParameters.Split(new char[] {' '});
            var parametersEnumerator = parameters.GetEnumerator();
            // bring the enumerator to the 1. element
            parametersEnumerator.MoveNext();
            foreach (char modeChar in mode) {
                switch (modeChar) {
                    case '+':
                        action = ChannelModeChangeAction.Set;
                        break;
                    case '-':
                        action = ChannelModeChangeAction.Unset;
                        break;
                    default:
                        ChannelModeInfo modeInfo = null;
                        modeMap.TryGetValue(modeChar, out modeInfo);
                        if (modeInfo == null) {
                            // modes not specified in CHANMODES are expected to
                            // always have parameters
                            modeInfo = new ChannelModeInfo((ChannelMode) modeChar, ChannelModeHasParameter.Always);
                        }

                        string parameter = null;
                        var channelMode = modeInfo.Mode;
                        if (!Enum.IsDefined(typeof(ChannelMode), channelMode)) {
                            channelMode = ChannelMode.Unknown;
                        }
                        var hasParameter = modeInfo.HasParameter;
                        if (hasParameter == ChannelModeHasParameter.Always ||
                            (hasParameter == ChannelModeHasParameter.OnlySet &&
                             action == ChannelModeChangeAction.Set)) {
                            parameter = (string) parametersEnumerator.Current;
                            parametersEnumerator.MoveNext();
                        }

                        modeChanges.Add(new ChannelModeChangeInfo() {
                            Action = action,
                            Mode = channelMode,
                            ModeChar = modeChar,
                            Parameter = parameter
                        });
                        break;
                }
            }

            return modeChanges;
        }
    }
}

