using CotcSdk;
using UnityEngine;

[RequireComponent(typeof(CotcGameObject))]
public class CotcManager : MonoBehaviour
{
    public static CotcManager Instance { get; private set; }

    public Gamer Gamer { get; private set; }

    private CotcGameObject cotc;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        cotc = GetComponent<CotcGameObject>();
    }

    public void LoginAnonymously()
    {
        cotc.GetCloud().Done(cloud => {
            cloud.LoginAnonymously()
            .Done(gamer => {
                AuthenticationManager.Instance.AddToLog("Signed in anonymously (ID = " + gamer.GamerId + ")");
                GameData.Instance.CotcId = gamer.GamerId;
                GameData.Instance.CotcSecret = gamer.GamerSecret;
                Gamer = gamer;
                SerializationManager.Save(GameData.Instance, "PlayerData");
            }, ex => {
                CotcException error = (CotcException)ex;
                AuthenticationManager.Instance.AddToLog("Failed to login: " + error.ErrorCode + " (" + error.HttpStatusCode + ")");
                AuthenticationManager.Instance.AddToLog("Details: " + error.ErrorInformation);
            });
        });
    }

    public void NetworkLogin(LoginNetwork network, string networkId, string networkSecret)
    {
        if (Gamer == null)
        {
            cotc.GetCloud().Done(cloud =>
            {
                AuthenticationManager.Instance.AddToLog("Provider: " + network.Describe());
                AuthenticationManager.Instance.AddToLog("Network Id: " + networkId);
                AuthenticationManager.Instance.AddToLog("Network Secret: " + networkSecret);
                cloud.Login(
                    network: network.Describe(),
                    networkId: networkId,
                    networkSecret: networkSecret
                ).Done(gamer =>
                {
                    AuthenticationManager.Instance.AddToLog("Network signin succeded (ID = " + gamer.GamerId + ")");
                    GameData.Instance.CotcId = gamer.GamerId;
                    GameData.Instance.CotcSecret = gamer.GamerSecret;
                    Gamer = gamer;
                    SerializationManager.Save(GameData.Instance, "PlayerData");
                }, ex =>
                {
                    CotcException error = (CotcException)ex;
                    AuthenticationManager.Instance.AddToLog("Failed to login: " + error.ErrorCode + " (" + error.HttpStatusCode + ")");
                    AuthenticationManager.Instance.AddToLog("Details: " + error.ErrorInformation);

                    LoginAnonymously();
                });
            });
        }
        else
            Convert(network, networkId, networkSecret);
    }

    public void ResumeSession(string gamerId, string gamerSecret)
    {
        cotc.GetCloud().Done(cloud => {
            cloud.ResumeSession(
                gamerId,
                gamerSecret
            ).Done(gamer => {
                AuthenticationManager.Instance.AddToLog("Resume succeeded (ID = " + gamer.GamerId + ")");
                Gamer = gamer;
            }, ex => {
                CotcException error = (CotcException)ex;
                AuthenticationManager.Instance.AddToLog("Failed to resume: " + error.ErrorCode + " (" + error.HttpStatusCode + ")");

                LoginAnonymously();
            });
        });
    }

    public void Convert(LoginNetwork network, string networkId, string networkSecret)
    {
        Gamer.Account.Convert(network.Describe(), networkId, networkSecret)
        .Done(convertRes => {
            AuthenticationManager.Instance.AddToLog("Convert succeeded: " + convertRes.ToString());
        }, ex => {
            CotcException error = (CotcException)ex;
            AuthenticationManager.Instance.AddToLog("Failed to convert: " + error.ErrorCode + " (" + error.HttpStatusCode + ")");
        });
    }

    public void Logout()
    {
        cotc.GetCloud().Done(cloud => {
            cloud.Logout(Gamer).Done(result => {
                AuthenticationManager.Instance.AddToLog("Logout succeeded");
            }, ex => {
                CotcException error = (CotcException)ex;
                AuthenticationManager.Instance.AddToLog("Failed to logout: " + error.ErrorCode + " (" + error.HttpStatusCode + ")");
            });
        });
    }

    public Promise<PagedList<Score>> GetBestHighScores(int maxPerPage = 10, int pageCount = 1)
    {
        Promise<PagedList<Score>> promise = new Promise<PagedList<Score>>();

        Gamer.Scores.Domain("private").BestHighScores("storyMode", maxPerPage, pageCount).ForwardTo(promise);

        return promise;
    }

    public Promise<PagedList<Score>> GetCenteredScores(int count = 10)
    {
        Promise<PagedList<Score>> promise = new Promise<PagedList<Score>>();

        Gamer.Scores.Domain("private").PagedCenteredScore("storyMode", count).ForwardTo(promise);

        return promise;
    }

    public Promise<int> GetScoreRank(long score)
    {
        Promise<int> promise = new Promise<int>();

        Gamer.Scores.Domain("private").GetRank(score, "storyMode").ForwardTo(promise);

        return promise;
    }

    public Promise<PostedGameScore> PostScore(long score, ScoreOrder order = ScoreOrder.LowToHigh)
    {
        Promise<PostedGameScore> promise = new Promise<PostedGameScore>();

        Gamer.Scores.Domain("private").Post(score, "storyMode", order).ForwardTo(promise);

        return promise;
    }
}
