using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;

namespace ExamApp.Config;

public class FirebaseAuthentication
{
    private static FirebaseAuth firebaseAuth;

    public static void InitializeFirebase()
    {
        var credentials = GoogleCredential.FromFile("demofrirst-firebase-adminsdk-8z930-0350448e6d.json");
        var firebaseApp = FirebaseApp.Create(new AppOptions
        {
            Credential = credentials
        });
        firebaseAuth = FirebaseAuth.GetAuth(firebaseApp);
    }

    public static async Task<string> VerifyIdTokenAsync(string idToken)
    {
        var decodedToken = await firebaseAuth.VerifyIdTokenAsync(idToken);
        return decodedToken.Uid;
    }
    
    public static async Task<UserRecord> GetUserRecordAsync(string userId)
    {
        var user = await FirebaseAuth.DefaultInstance.GetUserAsync(userId);
        return user;
    }
}