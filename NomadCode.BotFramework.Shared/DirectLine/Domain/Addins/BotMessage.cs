﻿#if __IOS__ || __ANDROID__

using System;
using System.Linq;
using System.Collections.Generic;


namespace NomadCode.BotFramework
{
	public class BotMessageAttachemnt
	{
		public Attachment Attachment { get; set; }
		public CardContent Content { get; set; }
		public BotMessageAttachemnt () { }
		public BotMessageAttachemnt (Attachment attachment, CardContent content) { Attachment = attachment; Content = content; }
	}

	public class BotMessage : IComparable<BotMessage>, IEquatable<BotMessage>
	{
#if __IOS__
        public nfloat CellHeight { get; set; }

        Foundation.NSAttributedString _attributedText;

        public Foundation.NSAttributedString AttributedText => _attributedText ?? (_attributedText = Activity?.Text?.GetMessageAttributedString ());

#elif __ANDROID__
		public float CellHeight { get; set; }

		string _attributedText;

		public string AttributedText => _attributedText ?? (_attributedText = Activity?.Text/*?.GetMessageAttributedString ()*/);
#endif

		public bool Head { get; set; }

		public Activity Activity { get; private set; }

		public bool HasText => !string.IsNullOrEmpty (Activity?.Text);

		public bool HasAtachments => Activity?.Attachments?.Count > 0;

		//bool? _hasCards;
		//public bool HasCards => _hasCards ?? (_hasCards = Attachments.Any (a => a.CardType != CardTypes.Unknown)).Value;



		//public int ButtonCount => Buttons.Count;

		//List<CardAction> _buttons;
		//public List<CardAction> Buttons => _buttons ?? (_buttons = HasCards ? Attachments.Where (a => a.CardType != CardTypes.Unknown).SelectMany (c => c.Attachment.GetButtons ()).ToList () : new List<CardAction> ());


		List<BotMessageAttachemnt> _attachments;

		public List<BotMessageAttachemnt> Attachments => _attachments ?? (_attachments = Activity?.Attachments?.Select (a => new BotMessageAttachemnt (a, a.GetContent ())).ToList () ?? new List<BotMessageAttachemnt> ());


		//public int ButtonCount

		//public bool HasButtons => Activity.Attachments.

		// TODO: cache this and reset when activity is updated
		public DateTime? LocalTimeStamp => Activity.Timestamp?.ToLocalTime () ?? Activity.LocalTimestamp?.LocalDateTime;


		public BotMessage (Activity activity) => Activity = activity;


		public int CompareTo (BotMessage other)
		{
			if (!string.IsNullOrEmpty (Activity?.Id) && !string.IsNullOrEmpty (other?.Activity?.Id))
			{
				return Activity.Id.CompareTo (other.Activity.Id);
			}

			return string.IsNullOrEmpty (Activity?.Id) ? 1 : -1;
		}


		public bool Equals (BotMessage other)
		{
			// HACK: This is nasty, but I want to be able to compare the message based on timestamp
			//       However, even if I set the timestamp, the server re-sets it so they tend to be
			//       a couple seconds off.  The server also sets the Id, so I don't have that when
			//       originally creating the Activity.
			if (string.IsNullOrEmpty (Activity?.Id) || string.IsNullOrEmpty (other?.Activity?.Id))
			{
				if ((Activity?.LocalTimestamp.HasValue ?? false) && (other?.Activity?.LocalTimestamp.HasValue ?? false))
				{
					return Activity.LocalTimestamp.Value.Equals (Activity.LocalTimestamp.Value);
				}

				return ((Activity?.Text?.Equals (other?.Activity?.Text) ?? false) &&
						(Activity?.Timestamp?.Date.Equals (other?.Activity?.Timestamp?.Date) ?? false) &&
						(Activity?.Timestamp?.Hour.Equals (other?.Activity?.Timestamp?.Hour) ?? false) &&
						(Activity?.Timestamp?.Minute.Equals (other?.Activity?.Timestamp?.Minute) ?? false));
			}

			return Activity.Id.Equals (other.Activity.Id);
		}


		public void Update (Activity activity) => Activity = activity;
	}
}

#endif