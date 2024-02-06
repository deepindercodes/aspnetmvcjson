﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web.Hosting;
using System.Web.Script.Serialization;

namespace aspnetmvcjson.Models
{
    public class jsonData
    {
        public string pk { get; set; }
        public List<Article> articles { get; set; }

        JavaScriptSerializer serializer = new JavaScriptSerializer();

        string jsonFilePath = HostingEnvironment.MapPath("/db/data.json");

        public List<Article> GetArticles()
        {
            if (File.Exists(jsonFilePath))
            {
                jsonData obj_jsonData = new jsonData();

                string jsonData = File.ReadAllText(jsonFilePath);

                obj_jsonData = serializer.Deserialize<jsonData>(jsonData);

                return obj_jsonData.articles.Where(u => u.status == "E").ToList();
            }
            else
            {
                return new List<Article>();
            }
        }

        public void AddArticle(Article article)
        {
            string articletitle = article.articletitle;
            string articleauthor = article.articleauthor;
            string articlebody = article.articlebody;
            string articleimage = article.articleimage;

            jsonData obj_jsonData = new jsonData();

            List<Article> objArticles = new List<Article>();

            

            Int32 pk = 1;

            if (File.Exists(jsonFilePath))
            {
                string jsonDataText = File.ReadAllText(jsonFilePath);

                obj_jsonData = serializer.Deserialize<jsonData>(jsonDataText);

                pk = Convert.ToInt32(obj_jsonData.pk);

                pk = pk + 1;

                objArticles = obj_jsonData.articles;
            }

            Article objArticle = new Article();

            objArticle.id = pk.ToString();
            objArticle.articletitle = articletitle;
            objArticle.articleauthor = articleauthor;
            objArticle.articlebody = articlebody;
            objArticle.articleimage = articleimage;
            objArticle.createdonutc = DateTime.UtcNow.ToString();
            objArticle.status = "E";

            obj_jsonData.pk = pk.ToString();

            objArticles.Add(objArticle);
            obj_jsonData.articles = objArticles;

            string jsonData = serializer.Serialize(obj_jsonData);

            File.WriteAllText(jsonFilePath, jsonData);
        }

        public void EditArticle(Article article)
        {
            string articletitle = article.articletitle;
            string articleauthor = article.articleauthor;
            string articlebody = article.articlebody;
            string articleimage = article.articleimage;

            jsonData obj_jsonData = new jsonData();

            List<Article> objArticles = new List<Article>();

            string jsonData = File.ReadAllText(jsonFilePath);

            obj_jsonData = serializer.Deserialize<jsonData>(jsonData);

            Article objArticle = obj_jsonData.articles.Where(u => u.id == article.id.ToString()).FirstOrDefault();

            objArticle.articletitle = articletitle;
            objArticle.articleauthor = articleauthor;
            objArticle.articlebody = articlebody;
            objArticle.articleimage = articleimage;
            objArticle.modifiedonutc = DateTime.UtcNow.ToString();

            jsonData = serializer.Serialize(obj_jsonData);

            File.WriteAllText(jsonFilePath, jsonData);

        }

        public void DeleteArticle(string id)
        {
            jsonData obj_jsonData = new jsonData();

            List<Article> objArticles = new List<Article>();

            string jsonData = File.ReadAllText(jsonFilePath);

            obj_jsonData = serializer.Deserialize<jsonData>(jsonData);

            Article objArticle = obj_jsonData.articles.Where(u => u.id == id.ToString()).FirstOrDefault();

            obj_jsonData.articles.Remove(objArticle);

            jsonData = serializer.Serialize(obj_jsonData);

            File.WriteAllText(jsonFilePath, jsonData);
        }
    }


    public partial class Article
    {
        
        public string id { get; set; }

        [Display(Name="Title")]
        [Required]
        public string articletitle { get; set; }

        [Display(Name = "Author")]
        [Required]
        public string articleauthor { get; set; }

        [Display(Name = "Body")]
        [Required]
        public string articlebody { get; set; }


        public string articleimage { get; set; }
        public string createdonutc { get; set; }
        public string modifiedonutc { get; set; }
        public string status { get; set; }
    }
}