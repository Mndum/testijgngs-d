using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Cysharp.Threading.Tasks;
using UnityEngine;

public static class EmailPrefabManager
{
	public class EmailPrefab : IKey<string>
	{
		public delegate string TextOverride(string textKey, NewEmail email);

		public class Builder
		{
			private readonly EmailPrefab _emailPrefab;

			public Builder(string keyId)
			{
				_emailPrefab = new EmailPrefab(keyId);
			}

			public Builder MarkSpam()
			{
				_emailPrefab.IsSpam = true;
				return this;
			}

			public Builder WithOnReadEvent(EmailEventTarget onReadEvent)
			{
				_emailPrefab.OnReadEvent = onReadEvent;
				return this;
			}

			public Builder WithTextOverride(TextOverride textOverride)
			{
				_emailPrefab.MainTextOverride = textOverride;
				return this;
			}

			public Builder WithSenderOverride(TextOverride senderOverride)
			{
				_emailPrefab.SenderTextOverride = senderOverride;
				return this;
			}

			public Builder WithTopicOverride(TextOverride topicOverride)
			{
				_emailPrefab.TopicTextOverride = topicOverride;
				return this;
			}

			public EmailPrefab Build()
			{
				return _emailPrefab;
			}
		}

		public string KeyId { get; private set; }

		public bool IsSpam { get; private set; }

		public EmailEventTarget OnReadEvent { get; private set; }

		public TextOverride TopicTextOverride { get; private set; }

		public TextOverride SenderTextOverride { get; private set; }

		public TextOverride MainTextOverride { get; private set; }

		string IKey<string>.Key => KeyId;

		public EmailPrefab(string keyId, bool isSpam = false)
		{
			KeyId = keyId;
			IsSpam = isSpam;
		}

		public static implicit operator string(EmailPrefab email)
		{
			return email.KeyId;
		}

		public static Builder StartBuilder(string keyId)
		{
			return new Builder(keyId);
		}
	}

	public class EmailPrefabManagerData : PrefabManagerDataOneToN<string, EmailPrefab, NewEmail>
	{
		protected override EmailPrefab GenerateEmpty(string key)
		{
			return new EmailPrefab(key);
		}
	}

	public class CocktractPartnerEmailContainer
	{
		public readonly EmailPrefab RefusalFirstEmail;

		public readonly EmailPrefab GoldRefusalEmail;

		public readonly EmailPrefab GoldExpiryGenericEmail;

		public readonly EmailPrefab FailFirstEmail;

		public readonly EmailPrefab FailGenericEmail;

		public readonly EmailPrefab Milestone1Email;

		public readonly EmailPrefab Milestone2Email;

		public CocktractPartnerEmailContainer(string companyId)
		{
			RefusalFirstEmail = new EmailPrefab(companyId + "RefusalFirst");
			GoldRefusalEmail = new EmailPrefab(companyId + "GoldRefusalGeneric", isSpam: true);
			GoldExpiryGenericEmail = new EmailPrefab(companyId + "GoldExpiryGeneric", isSpam: true);
			FailFirstEmail = new EmailPrefab(companyId + "FailFirst");
			FailGenericEmail = new EmailPrefab(companyId + "FailGeneric", isSpam: true);
			Milestone1Email = new EmailPrefab(companyId + "Milestone1");
			Milestone2Email = new EmailPrefab(companyId + "Milestone2");
		}
	}

	private static EmailPrefabManagerData data = new EmailPrefabManagerData();

	public static readonly EmailPrefab FriendEmail = EmailPrefab.StartBuilder("Friend").WithOnReadEvent(FungusBlocks.Email_Friend_Email).Build();

	public static readonly EmailPrefab BoBroganEmail = EmailPrefab.StartBuilder("BoBrogan").WithOnReadEvent(FungusBlocks.Email_Bo_Brogan_Podcast).Build();

	public static readonly EmailPrefab JoinUsEmail = EmailPrefab.StartBuilder("JoinUs").WithOnReadEvent(FungusBlocks.Email_Join_Us_Email).Build();

	public static readonly EmailPrefab JoinUsEmail2 = EmailPrefab.StartBuilder("JoinUs2").WithOnReadEvent(FungusBlocks.Email_Join_Us_Email_2).Build();

	public static readonly EmailPrefab JoinUsEmail2Rejected = new EmailPrefab("JoinUs2Rejected");

	public static readonly EmailPrefab JoinUsEmail3 = EmailPrefab.StartBuilder("JoinUs3").WithOnReadEvent(FungusBlocks.Email_Join_Us_Email_3).WithTextOverride((string key16, NewEmail email) => LOC.Emails.GS(key16, BrowserPopup.FormatLink(JoinUsBlogPrefabManager.Blog1.GetEntryLoginUrl(), LOC.UI.ClickHere)))
		.Build();

	public static readonly EmailPrefab JoinUsEmail4 = EmailPrefab.StartBuilder("JoinUs4").WithOnReadEvent(FungusBlocks.Email_Join_Us_Email_4).WithTextOverride((string key16, NewEmail email) => LOC.Emails.GS(key16, BrowserPopup.FormatLink(JoinUsBlogPrefabManager.Blog2.GetEntryLoginUrl(), LOC.UI.ClickHere)))
		.Build();

	public static readonly EmailPrefab JoinUsEmail5 = EmailPrefab.StartBuilder("JoinUs5").WithOnReadEvent(FungusBlocks.Email_Join_Us_Email_5).WithTextOverride((string key16, NewEmail email) => LOC.Emails.GS(key16, BrowserPopup.FormatLink(JoinUsBlogPrefabManager.Blog3.GetEntryLoginUrl(), LOC.UI.ClickHere)))
		.Build();

	public static readonly EmailPrefab JoinUsEmail6 = EmailPrefab.StartBuilder("JoinUs6").WithOnReadEvent(FungusBlocks.Email_Join_Us_Email_6).WithTextOverride((string key16, NewEmail email) => LOC.Emails.GS(key16, BrowserPopup.FormatLink(JoinUsBlogPrefabManager.Blog4.GetEntryLoginUrl(), LOC.UI.ClickHere)))
		.Build();

	public static readonly EmailPrefab JoinUsEmail7_BigDaddy1 = EmailPrefab.StartBuilder("JoinUs7_BigDaddy1").WithOnReadEvent(FungusBlocks.Email_Join_Us_Email_BigDaddy1).WithTextOverride((string key16, NewEmail email) => LOC.Emails.GS(key16, BrowserPopup.FormatLink(JoinUsBlogPrefabManager.BlogBigDaddy1.GetEntryLoginUrl(), LOC.UI.ClickHere)))
		.Build();

	public static readonly EmailPrefab JoinUsEmail7_BigDaddy4 = EmailPrefab.StartBuilder("JoinUs7_BigDaddy4").WithOnReadEvent(FungusBlocks.Email_Join_Us_Email_BigDaddy4).WithTextOverride((string key16, NewEmail email) => LOC.Emails.GS(key16, BrowserPopup.FormatLink(JoinUsBlogPrefabManager.BlogBigDaddy2.GetEntryLoginUrl(), LOC.UI.Blog_BlogLinkEmailInsertHyperlinkText)))
		.Build();

	public static readonly EmailPrefab JoinUsEmail7_BigDaddy9 = EmailPrefab.StartBuilder("JoinUs7_BigDaddy9").WithOnReadEvent(FungusBlocks.Email_Join_Us_Email_BigDaddy9).WithTextOverride((string key16, NewEmail email) => LOC.Emails.GS(key16, BrowserPopup.FormatLink(JoinUsBlogPrefabManager.BlogBigDaddy3.GetEntryLoginUrl(), LOC.UI.ClickHere)))
		.Build();

	public static readonly EmailPrefab JoinUsEmail7_Richard1 = EmailPrefab.StartBuilder("JoinUs7_Richard1").WithOnReadEvent(FungusBlocks.Email_Join_Us_Email_Richard1).WithTextOverride((string key16, NewEmail email) => LOC.Emails.GS(key16, BrowserPopup.FormatLink(JoinUsBlogPrefabManager.BlogRichard1.GetEntryLoginUrl(), LOC.UI.ClickHere)))
		.Build();

	public static readonly EmailPrefab JoinUsEmail7_Richard4 = EmailPrefab.StartBuilder("JoinUs7_Richard4").WithOnReadEvent(FungusBlocks.Email_Join_Us_Email_Richard4).WithTextOverride((string key16, NewEmail email) => LOC.Emails.GS(key16, BrowserPopup.FormatLink(JoinUsBlogPrefabManager.BlogRichard2.GetEntryLoginUrl(), LOC.UI.ClickHere)))
		.Build();

	public static readonly EmailPrefab FriendEmail2 = EmailPrefab.StartBuilder("Friend2").WithOnReadEvent(FungusBlocks.Email_Friend_Email2).Build();

	public static readonly EmailPrefab DoctorEmail = EmailPrefab.StartBuilder("Doctor").WithOnReadEvent(FungusBlocks.Email_Doctor_Email).Build();

	public static readonly EmailPrefab BitchEmail = EmailPrefab.StartBuilder("Bitch1").WithOnReadEvent(FungusBlocks.Email_BitchEmail1).Build();

	public static readonly EmailPrefab BitchEmail2 = EmailPrefab.StartBuilder("Bitch2").WithOnReadEvent(FungusBlocks.Email_BitchEmail2).Build();

	public static readonly EmailPrefab BitchEmail3 = EmailPrefab.StartBuilder("Bitch3").WithOnReadEvent(FungusBlocks.Email_BitchEmail3).Build();

	public static readonly EmailPrefab SlowmodeEmail = EmailPrefab.StartBuilder("Slowmode").WithOnReadEvent(FungusBlocks.Email_SlowmodeUnlocked).Build();

	public static readonly EmailPrefab CockTwitchEmail = EmailPrefab.StartBuilder("CockTwitch").MarkSpam().WithOnReadEvent(new CockTwitchManager.CocktwitchEmailSubReadEventTarget())
		.Build();

	public static readonly EmailPrefab FedUpNewDeliveryEmail = new EmailPrefab("FedUpNewDelivery", isSpam: true);

	public static readonly EmailPrefab FedUpConsolidationEmail = new EmailPrefab("FedUpConsolidation", isSpam: true);

	public static readonly EmailPrefab ErrorEmail = new EmailPrefab("Error");

	public static readonly CocktractPartnerEmailContainer LuxeEmails = new CocktractPartnerEmailContainer(CocktractPartnerPrefabManager.luxeId);

	public static readonly CocktractPartnerEmailContainer StilouEmails = new CocktractPartnerEmailContainer(CocktractPartnerPrefabManager.stilouId);

	public static readonly CocktractPartnerEmailContainer VolksAutoEmails = new CocktractPartnerEmailContainer(CocktractPartnerPrefabManager.volksAutoId);

	public static readonly CocktractPartnerEmailContainer MonizmedEmails = new CocktractPartnerEmailContainer(CocktractPartnerPrefabManager.monizmedId);

	public static readonly CocktractPartnerEmailContainer DiabeteEmails = new CocktractPartnerEmailContainer(CocktractPartnerPrefabManager.diabeteId);

	public static readonly CocktractPartnerEmailContainer CazinoEmails = new CocktractPartnerEmailContainer(CocktractPartnerPrefabManager.cazinoId);

	public static readonly EmailPrefab LabourOfficeEmail = new EmailPrefab("LabourOffice");

	public static readonly EmailPrefab CockAdEmail = new EmailPrefab("CockAd");

	public static readonly EmailPrefab BigDaddyHurtEmail1 = new EmailPrefab("BigDaddyHurt1");

	public static readonly EmailPrefab BigDaddyHurtEmail1Alt = new EmailPrefab("BigDaddyHurt1Alt");

	public static readonly EmailPrefab BigDaddyHurtEmail2 = new EmailPrefab("BigDaddyHurt2");

	public static readonly EmailPrefab CasinoEmail = new EmailPrefab("Casino");

	public static readonly EmailPrefab CocktractsIntroductionEmail = new EmailPrefab("CocktractsIntroduction");

	public static List<KeyValuePair<string, Email>> AllOldEmails = new Dictionary<string, Email>
	{
		[FriendEmail] = new Email("emilygrossberg@safespace.ic", "Hi Anon!", "Do you still remember me? It’s your highschool friend Emily! Together with others we are organizing a class meetup! Renting a place for 30 people these days is a big problem, but don’t worry! Me and Taylor will get everything ready. You only need to return us part of the expense, something like 100$. We got some experience in creating events like that and if you follow our Faceberg page you know it for sure. If you don’t follow us yet, we encourage you to do this now by clicking here. If you have any questions about this be sure to send us an e-mail. Our bank account number for you to send us that hundred is 857240756612. Remember to write down your name on a transfer receipt. \r\nSee ya!\r\n"),
		[BoBroganEmail] = new Email("bbfreepodcast@pro.ic", "LATEST BO BROGAN PODCAST FOR FREE!", "Specially for you we are giving out one episode of the Bo Brogan podcast for free!\n In this episode Bo’s guest is 96 years old known astrophysic and gulag survivor prof. Nigevchenko.\n\n <b>Click here to listen to the full audio</b>"),
		[JoinUsEmail] = new Email("saawmohiygdneoisngosnmgiod@pro.ic", "Do you want to join... the revolution?", "This country... ROTTEN beyond repair... join us... \r\nThis world... CORRUPTED without recognition... join us...\r\nOur government... UNSALVAGEABLE after rejecting the vision of Founding Fathers... join us...\r\njoin us... join us... join us... join us... join us... join us... join us... join us... join us... \r\njoin us... join us... join us... join us... join us... join us... join us... join us... join us... \r\njoin us... join us... join us... join us... join us... join us... join us... join us... join us... \r\nIf you do want to join us... just replay with... I join us...\r\nif you are thinking about joining us... stop thinking... and join us...\r\nif you are hesitant about joining us... give it some thought... and join us... \r\nif you don’t want to join us... you will want to join us in the future... so join us...\r\nif you hate us and the sole thought of joining us is making you vomit... stop being a bitch... and join us...\r\nAs a real American patriot your duty is to join us...\r\nIf you are not American... join us anyway...\r\nWe are everywhere...\r\nMillions of patriots all around the world... united by a single dream...\r\nDoesn’t matter where are you from... what your nationality is... we are all Americans...\r\nJOIN. US.\r\nJoin... us...\r\nWe are... Us...\r\n"),
		[FriendEmail2] = new Email("emilygrossberg@safespace.ic", "Hi Again!", "Hi, it’s me, Emily again. I don’t know if you got the last mail from me, but we are organizing a class meetup. It’s only 100$ and so far almost everyone agreed to come! I know you were kinda quiet in highschool, but I assure you that many people are interested in how you are doing :) If you want to know more check out my previous mail!"),
		[DoctorEmail] = new Email("drblaubleim@safespace.ic", "New appointment", "Hi Anon.\r\nYour mom filled and paid for your new appointment at my office. I heard that you stopped taking your meds? It’s not like you were supposed to take them forever, but I thought I told you to consult it with me beforehand. No biggie, just come to me and we will talk about it son."),
		[BitchEmail] = new Email("kfz35432390@pro.ic", "I KNOW WHAT YOU ARE DOING", "You motherfucker... Did you really think you could just...\r\njust do this shit? On my watch? I've been using CockTwitch for a few years now, but this...\r\nIn this business the most important thing is respect and this is the shit you have been up to? Dp you even have a SINGLE IDEA what sex workets went trough because of likes of you and you’re plastic BITCH? \r\nSTOP THKS NOW!!!!!!!!!1111!!!!!!!!!!!!!!!!!!\r\n"),
		[BitchEmail2] = new Email("kfz35432391@pro.ic", "WHY AREn’T YOu ANSWERING????????????????", "ANSWER ME BITCH! I even changed my email you FUCKER!!!!!!!!! Yuo blocked me right? you fucking blocked me, didn’t you? I will found you motherfucker, YOU ARE TAKING BREAD OUT OF THe TABLES OF HARD WORKING AND PRIDE WOMEN!!111111111111!!!!!!!!!! I lLost 100 subsrcibers in 2 weeks. 2 WEEKS!!!!!!11111 You know how har dI worked for this? Ypu probably are behind these fucking comments about my weight gain, huh? Y’all can go fuck yoursleds. YOU. AND. YOU.R BITCH. BOTH DEAD. WATCH YOUR BACK."),
		[BitchEmail3] = new Email("kfz4093673948@pro.ic", "IM COMING FOR YOU CRACKER!!!!!!!!!!11!!!1!!!1", "I SPENT ALL OF MY CRYPTO CURRENCNY TO HIRE A HACKER THAT WILL FIND YOU. oNCE HE DOXXES YOU, YOU ARE DEAD MEAT. FUCKkkkk YOu!!!!!!!!!!! i WILL STAB YOU WITH MY CARAMBIT. i will show you what a REAL woman can do and you expect your bouncy white balls on a silver platter.!"),
		[SlowmodeEmail] = new Email("noreply@cocktwitch.ic", "Congratulations on reaching over {0} followers! ", "You've reached over {0} followers today!\nWe know that managing such a big audience is hard, that's why our engineers have come up with " + "SLOWMODE".TMPColorSurround(Color.red) + ". It limits the amount of messages, so you can still fully enjoy their eloquence.\nYou can toggle it during your streams as long as you keep over {0} followers.\n\nThis email was generated automatically. Please do not respond!"),
		[CockTwitchEmail] = new Email("support@cocktwitch.ic", "Subs", "Congratulations!\n Today from subscriptions you've earned:\n {0}*{1}$={2}$\n\nIn total {2}$ has been deposited into your account when you opened this email!\n\nThis email was generated automatically. Please do not respond!"),
		[LabourOfficeEmail] = new Email("labour@office.gov", "Unemployment Notice", "Hello dear citizen of our great country! \n\nWe write to you to inform you that your unemployment benefit has just expired because you failed in taking up one of our specially prepared job offers. Therefore you will no longer benefit from your job status until you finish a period of reemployment in one of our private sector partners. For more details visit your local labour office. Remember to take with you your ID and the necessary paperwork (documents No 14a, 17b, 25a and 25b) which you can download from our website and of course your CV. We wish you all the best in your future workplace. \n\nSincerely, Labour office of US This message has been generated automatically. \nPlease, do not respond."),
		[CockAdEmail] = new Email("cbzsbcifu@cock.cum", "ENHANCE YOUR PENIS BY 12CM", "Girls are making fun of you? Your wife is cheating with bigger men? We have a solution just for you! Thanks to the newest technology our miraculous drug can make you grow your member even 50% of your original size! Our nanobots are able to rearrange muscle fibers inside your cock to make it throb longer and harder. Wanna know more? Pay us a symbolic sum of 1000$ and we will contact you about a possible test trial! \nOur Bitcoin adress: oiwnfoainaosinfoiasf9isndklfs0nslgns02."),
		[BigDaddyHurtEmail1] = new Email("bigdaddyhurt1177@gooblygook.ic", "Nice stream man :)", "Hi thre buddy. \nSaw your stream last night :) Good stuff. I had some troubles contacting you, but I somehow manadged :) Wanna sell your sexbot? Never seen a model like that and I’m something of a collectionner. I’m willing to give ya for it 10k easy. If you don’t wanna sell it, no stress :) I’m a cool guy, so you don’t need to worre about me ratting you out, just having a friendly chat here. Just be careful out there budy :) not everyone is such a pure soul like me. \n\nBye Bye :)"),
		[BigDaddyHurtEmail2] = new Email("bigdaddyhurt1177@gooblygook.ic", "No Response?", "Hi again. \nDid you read my last email? Maybe you are just ignoring me. Doesn’t matter. Don’t worry, I’m not mad :) I’m not gonna bother you with offers anymore. Just want to give ya a link to online store you might be interested with :) ladyparts.ic\nContent on the website is available only to chosen IPs so its safe. You can buy there some parts for your bot and make her have more… functions :)\nHave a nice day man :) I’m gonna delete this email address now for safety reasons :)"),
		[CasinoEmail] = new Email("cazinoparadize@sc2137.ic", "NEW INTERNET CASINO", "Do You Want To Become A Billionaire In Just A 10 Seconds???!!!! Now You Can And The Stakes Never Were Higher!!! Our New Offshore Internet Crypto-Mining Casino Is The Best Place To Win Big And Commit Tax Fraud. No Annoying People Telling You To Stop Drinking, No Robberies At Gunpoint! Just Use Our Link casino.ic Get 100 Free Tokens And Start The Fun!")
	}.ToList();

	public static List<EmailPrefab> AllEmails
	{
		get
		{
			List<EmailPrefab> list = new List<EmailPrefab>();
			list.AddRange(from x in typeof(EmailPrefabManager).GetFields(BindingFlags.Static | BindingFlags.Public)
				where x.FieldType == typeof(EmailPrefab)
				select (EmailPrefab)x.GetValue(null));
			List<FieldInfo> cpecFields = (from f in typeof(CocktractPartnerEmailContainer).GetFields(BindingFlags.Instance | BindingFlags.Public)
				where f.FieldType == typeof(EmailPrefab)
				select f).ToList();
			list.AddRange((from x in typeof(EmailPrefabManager).GetFields(BindingFlags.Static | BindingFlags.Public)
				where x.FieldType == typeof(CocktractPartnerEmailContainer)
				select (CocktractPartnerEmailContainer)x.GetValue(null)).SelectMany((CocktractPartnerEmailContainer x) => cpecFields.Select((FieldInfo y) => (EmailPrefab)y.GetValue(x))));
			return list;
		}
	}

	public static void FillPrefabData(List<NewEmail> emails)
	{
		data.FillWithPrefabs(emails);
	}

	public static void FillPrefabData(NewEmail email)
	{
		data.FillWithPrefab(email);
	}

	[LoadableStaticMethod(null)]
	private static UniTask Load()
	{
		data.Init(AllEmails);
		return UniTask.CompletedTask;
	}

	public static NewEmail GetEmail(string emailId, params string[] specialValues)
	{
		return GetEmail(emailId, specialValues.ToList());
	}

	public static NewEmail GetEmail(EmailPrefab emailId, List<string> specialValues = null)
	{
		NewEmail newEmail = new NewEmail(emailId, specialValues);
		newEmail.InitPrefab(emailId);
		return newEmail;
	}

	public static NewEmail GetEmail(string emailId, List<string> specialValues = null)
	{
		NewEmail newEmail = new NewEmail(emailId, specialValues);
		FillPrefabData(newEmail);
		return newEmail;
	}
}
