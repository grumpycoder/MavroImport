using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FileHelpers;

namespace MavroImport
{
    class Program
    {
        public enum ConfidentialityType
        {
            EyesOnly = 1,
            OffTheRecord = 2,
            Confidential = 3,
            Open = 4,
            Security = 21
        }
        static void Main(string[] args)
        {

            ExtractLoadChapterPublications();
//            ExtractLoadPersonPublications();
            //ExtractLoadWebsitePublications();
            Console.WriteLine("Finished");
            Console.ReadKey();
        }

        private static void ExtractLoadWebsitePublications()
        {
            // Read file
            var engine = new FileHelperEngine<MavroRecord>();
            var results = engine.ReadFile(@"C:\temp\beholderArchiveUrl.csv").Where(r => r.RecordType == "WEBSITE");

            var records = results as IList<MavroRecord> ?? results.ToList();
            using (var db = new AppContext())
            {
                foreach (var record in records.Take(1))
                {
                    var website = db.MediaWebsiteEGroups.Find(Convert.ToInt32(record.Id));
                    Console.WriteLine("Creating item for WebSite: {0}: Id: {1}", website.Name, website.Id);
                    using (var client = new WebClient())
                    {
                        byte[] buffer = client.DownloadData(record.Url);

                        const int userId = 1;

                        var context = new MediaWebsiteEGroupContext()
                        {
                            MediaWebsiteEGroupId = website.Id,
                            ContextText = buffer,
                            MimeTypeId = 7,
                            DocumentExtension = ".pdf",
                            FileStreamID = Guid.NewGuid(),
                            FileName = "MavroImport-Website-" + website.Name + ".pdf",
                        };
                        db.MediaWebsiteEGroupContexts.Add(context);
                        db.SaveChanges();
                    }
                }
            }
        }

        private static void ExtractLoadPersonPublications()
        {
            // Read file
            var engine = new FileHelperEngine<MavroRecord>();
            var results = engine.ReadFile(@"C:\temp\beholderArchiveUrl.csv").Where(r => r.RecordType == "PERSON");

            var records = results as IList<MavroRecord> ?? results.ToList();
            using (var db = new AppContext())
            {
                foreach (var record in records.Take(1))
                {
                    var beholderPerson = db.BeholderPeople.Find(Convert.ToInt32(record.Id));
                    var commonPerson = db.CommonPeople.Find(beholderPerson.PersonId);

                    Console.WriteLine("Creating item for Person: {0}: BeholderId: {1}", commonPerson.FullName, beholderPerson.Id);
                    using (var client = new WebClient())
                    {
                        byte[] buffer = client.DownloadData(record.Url);

                        const int userId = 1;

                        var rel = new PersonMediaPublishedRel()
                        {

                            PersonId = beholderPerson.Id,
                            RelationshipTypeId = 99,
                            MediaPublished = new MediaPublished()
                            {
                                MediaTypeId = 4,
                                ConfidentialityTypeId = 4,
                                DateCreated = DateTime.Now,
                                DateModified = DateTime.Now,
                                CreatedUserId = userId,
                                Name = "MavroImport-Person-" + commonPerson.LName
                            }
                        };
                        db.PersonMediaPublishedRels.AddOrUpdate(rel);
                        db.SaveChanges();

                        var context = new MediaPublishedContext()
                        {
                            ContextText = buffer,
                            MimeTypeId = 7,
                            DocumentExtension = ".pdf",
                            FileStreamID = Guid.NewGuid(),
                            FileName = "MavroImport-Person-" + commonPerson.FullName + ".pdf",
                            MediaPublishedId = rel.MediaPublishedId
                        };
                        db.MediaPublishedContexts.Add(context);
                        db.SaveChanges();


                    }
                }
            }
        }


        public static void ExtractLoadChapterPublications()
        {
            // Read file
            var engine = new FileHelperEngine<MavroRecord>();
            var results = engine.ReadFile(@"C:\temp\beholderArchiveUrl.csv").Where(r => r.RecordType == "CHAPTER");

            var records = results as IList<MavroRecord> ?? results.ToList().Take(10);
            using (var db = new AppContext())
            {
                foreach (var record in records)
                {
                    var chapter = db.Chapters.Find(Convert.ToInt32(record.Id));
                    Console.WriteLine("Creating publication for chapter: {0}: Id: {1}", chapter.ChapterName, chapter.Id);
                    using (var client = new WebClient())
                    {
                        byte[] buffer = client.DownloadData(record.Url);

                        const int userId = 1;

                        var rel = new ChapterMediaPublishedRel()
                        {
                            ChapterId = chapter.Id,
                            RelationshipTypeId = 99,
                            MediaPublished = new MediaPublished()
                            {
                                MediaTypeId = 4,
                                ConfidentialityTypeId = 4,
                                DateCreated = DateTime.Now,
                                DateModified = DateTime.Now,
                                CreatedUserId = userId,
                                Name = "MavroImport-Chapter-" + chapter.ChapterName
                            }
                        };
                        db.ChapterMediaPublishedRels.AddOrUpdate(rel);
                        db.SaveChanges();

                        var context = new MediaPublishedContext()
                        {
                            ContextText = buffer,
                            MimeTypeId = 7,
                            DocumentExtension = ".pdf",
                            FileStreamID = Guid.NewGuid(),
                            FileName = "MavroImport-Chapter-" + chapter.ChapterName + ".pdf",
                            MediaPublishedId = rel.MediaPublishedId
                        };
                        db.MediaPublishedContexts.Add(context);
                        db.SaveChanges();

                        Console.WriteLine("Created publication {0}", rel.MediaPublished);
                    }
                }
            }
        }

    }

    [DelimitedRecord(",")]
    public class MavroRecord
    {
        public String Id { get; set; }
        public String RecordType { get; set; }
        public String Url { get; set; }
    }
}
