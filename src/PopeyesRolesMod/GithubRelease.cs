using System;
using System.Collections.Generic;

namespace PopeyesRolesMod
{
    public class Author
    {
        public string Login { get; set; }
        public int Id { get; set; }
        public string Node_id { get; set; }
        public string Avatar_url { get; set; }
        public string Gravatar_id { get; set; }
        public string Url { get; set; }
        public string Html_url { get; set; }
        public string Followers_url { get; set; }
        public string Following_url { get; set; }
        public string Gists_url { get; set; }
        public string Starred_url { get; set; }
        public string Subscriptions_url { get; set; }
        public string Organizations_url { get; set; }
        public string Repos_url { get; set; }
        public string Events_url { get; set; }
        public string Received_events_url { get; set; }
        public string Type { get; set; }
        public bool Site_admin { get; set; }
    }

    public class Uploader
    {
        public string Login { get; set; }
        public int Id { get; set; }
        public string Node_id { get; set; }
        public string Avatar_url { get; set; }
        public string Gravatar_id { get; set; }
        public string Url { get; set; }
        public string Html_url { get; set; }
        public string Followers_url { get; set; }
        public string Following_url { get; set; }
        public string Gists_url { get; set; }
        public string Starred_url { get; set; }
        public string Subscriptions_url { get; set; }
        public string Organizations_url { get; set; }
        public string Repos_url { get; set; }
        public string Events_url { get; set; }
        public string Received_events_url { get; set; }
        public string Type { get; set; }
        public bool Site_admin { get; set; }
    }

    public class Asset
    {
        public string Url { get; set; }
        public int Id { get; set; }
        public string Node_id { get; set; }
        public string Name { get; set; }
        public object Label { get; set; }
        public Uploader Uploader { get; set; }
        public string Content_type { get; set; }
        public string State { get; set; }
        public int Size { get; set; }
        public int Download_count { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        public string Browser_download_url { get; set; }
    }

    public class GithubRelease
    {
        public string Url { get; set; }
        public string Assets_url { get; set; }
        public string Upload_url { get; set; }
        public string Html_url { get; set; }
        public int Id { get; set; }
        public Author Author { get; set; }
        public string Node_id { get; set; }
        public string Tag_name { get; set; }
        public string Target_commitish { get; set; }
        public string Name { get; set; }
        public bool Draft { get; set; }
        public bool Prerelease { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Published_at { get; set; }
        public List<Asset> Assets { get; set; }
        public string Tarball_url { get; set; }
        public string Zipball_url { get; set; }
        public string Body { get; set; }
    }


}
