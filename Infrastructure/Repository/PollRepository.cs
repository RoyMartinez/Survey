using Domain.Dto;
using Domain.Enums;
using Domain.Models;
using Infrastructure.Context;
using Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Repository
{
    public class PollRepository : BaseRepository<Answer>,IPollRepository
    {
        private readonly IConfiguration _configuration;
        public PollRepository(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        public IEnumerable<Question> GetQuestions(int SurveyId)
        {
            try
            {
                IQueryable<Survey> Surveys;
                List<Question> Response = new List<Question>();
                using (var db = new SurveyContext(_configuration))
                {
                    Surveys = (from q in db.Set<Survey>()
                               where q.Id == SurveyId
                               select q
                               )
                               .Include(q => q.SurveysXQuestions)
                               .ThenInclude(q => q.Question).ToList().AsQueryable();
                }
                foreach (var Survey in Surveys)
                {
                    var SurveysXQuestions = Survey.SurveysXQuestions;
                    var question = SurveysXQuestions.Select(q => q.Question);
                    Response.AddRange(question.Select(q => q));

                }
                return Response;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string SetAnswers(int SurveyId, List<AnswerRequestDto> Answers)
        {
            using (var db = new SurveyContext(_configuration))
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var time = DateTime.Now;
                        var Survey = (from q in db.Set<Survey>()
                                      where q.Id == SurveyId
                                      select q).FirstOrDefault();
                        if (Survey == null) return "Error Invalid survey Id";

                        long Count = (from q in db.Set<Document>()
                                       where q.CreationTime.Day == time.Day &&
                                             q.CreationTime.Month == time.Month &&
                                             q.CreationTime.Year == time.Year
                                       select q).Count()+1;
                        string code = $"{DateTime.Now.ToString("yyyyMMdd")}-{Count.ToString("000000000")}";


                        Document newDocument = new Document() 
                        {
                            CreationTime = time,
                            Code = code,
                        };

                        db.Set<Document>().Add(newDocument);
                        db.SaveChanges();

                        foreach (var answer in Answers)
                        {
                            var FindSurveyXQuestion = (from q in db.Set<SurveyXQuestion>()
                                          where q.QuestionId == answer.QuestionId &&
                                                q.SurveyId == SurveyId
                                          select q)
                                          .Include(q=> q.Question).FirstOrDefault();
                            var FindQuestion = FindSurveyXQuestion.Question;

                            Answer newAnswer = new Answer()
                            {
                                SurveyXQuestionId = FindSurveyXQuestion.Id,
                                DocumentId = newDocument.Id
                            };

                            switch (FindQuestion.Type)
                            {
                                case QuestionTypeEnum.Number:
                                    newAnswer.Number = Convert.ToInt64(answer.Answer);
                                    break;
                                case QuestionTypeEnum.DateTime:
                                    newAnswer.Date = Convert.ToDateTime(answer.Answer);
                                    break;
                                case QuestionTypeEnum.String:
                                    newAnswer.String = answer.Answer;
                                    break;
                            }
                            db.Set<Answer>().Add(newAnswer);
                            db.SaveChanges();
                        }

                        dbContextTransaction.Commit();
                        return newDocument.Code;
                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback();
                        return $"Error {ex.Message}";
                    }
                }
            }
        }

        public Document GetSurveyResponseByCode(string Code)
        {
            try
            {
                Document Documents;
                using (var db = new SurveyContext(_configuration))
                {
                    Documents = (from q in db.Set<Document>()
                                 where q.Code == Code
                                 select q)
                                 .Include(q => q.Answers)
                                 .ThenInclude(q => q.SurveyXQuestion)
                                 .ThenInclude(q=> q.Question)
                                 .Include(q => q.Answers)
                                .ToList().FirstOrDefault();
                }
                return Documents;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public IQueryable<Document> GetSurveyResponses()
        {
            try
            {
                IQueryable<Document> Documents;
                using (var db = new SurveyContext(_configuration))
                {
                    Documents = (from q in db.Set<Document>()
                                 select q)
                                .Include(q => q.Answers)
                                .ThenInclude(q => q.SurveyXQuestion)
                                .ThenInclude(q => q.Question)
                                .Include(q => q.Answers)
                                .ToList().AsQueryable(); 
                }

                return Documents;
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}
