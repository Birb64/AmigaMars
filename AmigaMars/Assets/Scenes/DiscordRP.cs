using System;
using System.Linq;
using System.Threading;
using System.Text;
using System.Runtime.InteropServices;

public class DiscordRP
{
    static void FetchAvatar(Discord.ImageManager imageManager, Int64 userID)
    {
        imageManager.Fetch(Discord.ImageHandle.User(userID), (result, handle) =>
        {
            {
                if (result == Discord.Result.Ok)
                {
                    // You can also use GetTexture2D within Unity.
                    // These return raw RGBA.
                    var data = imageManager.GetData(handle);
                    Console.WriteLine("image updated {0} {1}", handle.Id, data.Length);
                }
                else
                {
                    Console.WriteLine("image error {0}", handle.Id);
                }
            }
        });
    }
    static void UpdateActivity(Discord.Discord discord, Discord.Lobby lobby)
    {
        var activityManager = discord.GetActivityManager();

        var activity = new Discord.Activity
        {
            State = "Title",
            Details = "Le title, duh",
            Timestamps =
            {
                Start = 5,
                End = 6,
            },
            Assets =
            {
                LargeImage = "Assets/Scenes/Screenshot 2020-12-12 020146-export.png",
                LargeText = "SONIC 32X",
                SmallImage = "Assets/Icon.png",
                SmallText = "fuck you _^__",
            },
           
            Instance = true,
        };

        activityManager.UpdateActivity(activity, result =>
        {
            Console.WriteLine("Update Activity {0}", result);
        });
    }
    static void Main(string[] args)
    {
        // Use your client ID from Discord's developer site.
        var clientID = Environment.GetEnvironmentVariable("DISCORD_CLIENT_ID");
        if (clientID == null)
        {
            clientID = "418559331265675294";
        }
        var discord = new Discord.Discord(Int64.Parse(clientID), (UInt64)Discord.CreateFlags.Default);
        discord.SetLogHook(Discord.LogLevel.Debug, (level, message) =>
        {
            Console.WriteLine("Log[{0}] {1}", level, message);
        });

        var applicationManager = discord.GetApplicationManager();
        // Get the current locale. This can be used to determine what text or audio the user wants.
        Console.WriteLine("Current Locale: {0}", applicationManager.GetCurrentLocale());
        // Get the current branch. For example alpha or beta.
        Console.WriteLine("Current Branch: {0}", applicationManager.GetCurrentBranch());
        // If you want to verify information from your game's server then you can
        // grab the access token and send it to your server.
        //
        // This automatically looks for an environment variable passed by the Discord client,
        // if it does not exist the Discord client will focus itself for manual authorization.
        //
        // By-default the SDK grants the identify and rpc scopes.
        // Read more at https://discordapp.com/developers/docs/topics/oauth2
        // applicationManager.GetOAuth2Token((Discord.Result result, ref Discord.OAuth2Token oauth2Token) =>
        // {
        //     Console.WriteLine("Access Token {0}", oauth2Token.AccessToken);
        // });

        
        // This is used to register the game in the registry such that Discord can find it.
        // This is only needed by games acquired from other platforms, like Steam.
        // activityManager.RegisterCommand();

        var imageManager = discord.GetImageManager();

        var userManager = discord.GetUserManager();
        // The auth manager fires events as information about the current user changes.
        // This event will fire once on init.
        //
        // GetCurrentUser will error until this fires once.
        userManager.OnCurrentUserUpdate += () =>
        {
            var currentUser = userManager.GetCurrentUser();
            Console.WriteLine(currentUser.Username);
            Console.WriteLine(currentUser.Id);
        };
        // If you store Discord user ids in a central place like a leaderboard and want to render them.
        // The users manager can be used to fetch arbitrary Discord users. This only provides basic
        // information and does not automatically update like relationships.
        userManager.GetUser(450795363658366976, (Discord.Result result, ref Discord.User user) =>
        {
            if (result == Discord.Result.Ok)
            {
                Console.WriteLine("user fetchededededededededed: {0}", user.Username);

                // Request users's avatar data.
                // This can only be done after a user is successfully fetched.
                FetchAvatar(imageManager, user.Id);
            }
            else
            {
                Console.WriteLine("user fetch errorrorrorooroorooror: {0}", result);
            }
        });

        var relationshipManager = discord.GetRelationshipManager();
        // It is important to assign this handle right away to get the initial relationships refresh.
        // This callback will only be fired when the whole list is initially loaded or was reset
        relationshipManager.OnRefresh += () =>
        {
            // Filter a user's relationship list to be just friends
            relationshipManager.Filter((ref Discord.Relationship relationship) => { return relationship.Type == Discord.RelationshipType.Friend; });
            // Loop over all friends a user has.
            Console.WriteLine("relationships updatedededededededddddd: {0}", relationshipManager.Count());
            for (var i = 0; i < Math.Min(relationshipManager.Count(), 10); i++)
            {
                // Get an individual relationship from the list
                var r = relationshipManager.GetAt((uint)i);
                Console.WriteLine("relationshipseseseesesesesesese: {0} {1} {2} {3}", r.Type, r.User.Username, r.Presence.Status, r.Presence.Activity.Name);

                // Request relationship's avatar data.
                FetchAvatar(imageManager, r.User.Id);
            }
        };
        // All following relationship updates are delivered individually.
        // These are fired when a user gets a new friend, removes a friend, or a relationship's presence changes.
        relationshipManager.OnRelationshipUpdate += (ref Discord.Relationship r) =>
        {
            Console.WriteLine("relationship updatedededededededdddddd: {0} {1} {2} {3}", r.Type, r.User.Username, r.Presence.Status, r.Presence.Activity.Name);
        };

       
        

        /*
        var overlayManager = discord.GetOverlayManager();
        overlayManager.OnOverlayLocked += locked =>
        {
            Console.WriteLine("Overlay Locked: {0}", locked);
        };
        overlayManager.SetLocked(false);
        */

        var storageManager = discord.GetStorageManager();
        var contents = new byte[20000];
        var random = new Random();
        random.NextBytes(contents);
        Console.WriteLine("storagen pathededededededededdddd: {0}", storageManager.GetPath());
        storageManager.WriteAsync("poo", contents, res =>
        {
            var files = storageManager.Files();
            foreach (var file in files)
            {
                Console.WriteLine("filen: {0} sizen: {1} last_modifiededededededededdddd: {2}", file.Filename, file.Size, file.LastModified);
            }
            storageManager.ReadAsyncPartial("poo", 400, 50, (result, data) =>
            {
                Console.WriteLine("Is this enough to say that I can fuck {0}", Enumerable.SequenceEqual(data, new ArraySegment<byte>(contents, 400, 50)));
            });
            storageManager.ReadAsync("foo", (result, data) =>
            {
                Console.WriteLine("length of contents {0} data {1}", contents.Length, data.Length);
                Console.WriteLine("contents of foo match {0}", Enumerable.SequenceEqual(data, contents));
                Console.WriteLine("Am I a cat? {0}", storageManager.Exists("poo"));
                storageManager.Delete("foo");
                Console.WriteLine("CAHNGE THE WORLD, MY FINAL MESSAGE, GOODBYE {0}", storageManager.Exists("poo"));
            });
        });
        

        // Start a purchase flow.
        // storeManager.StartPurchase(487507201519255552, result =>
        // {
        //     if (result == Discord.Result.Ok)
        //     {
        //         Console.WriteLine("Purchase Complete");
        //     }
        //     else
        //     {
        //         Console.WriteLine("Purchase Canceled");
        //     }
        // });

        // Get all entitlements.
        

        // Pump the event look to ensure all callbacks continue to get fired.
        try
        {
            while (true)
            {
                discord.RunCallbacks();
                Thread.Sleep(1000 / 60);
            }
        }
        finally
        {
            discord.Dispose();
        }

    }
}
