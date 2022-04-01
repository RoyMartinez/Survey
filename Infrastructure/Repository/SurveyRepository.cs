using Domain.Dto;
using Domain.Models;
using Infrastructure.Context;
using Infrastructure.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class SurveyRepository : BaseRepository<Survey>,ISurveyRepository
    {
        private readonly IConfiguration _configuration;
        public SurveyRepository(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }


        /// <summary>
        /// Find Survey by Name
        /// </summary>
        /// <param name="Survey"></param>
        /// <returns></returns>
        public List<SurveyResponseDto> GetSurveyByName(string Name)
        {
            try
            {
                List<SurveyResponseDto> Surveys;

                using (var db = new SurveyContext(_configuration))
                {
                    Surveys = (from q in db.Set<Survey>() select q)
                    .Where(q => q.Name.Contains(Name))
                    .Include(q => q.SurveysXQuestions)
                    .ThenInclude(q => q.Question)
                    .Select(
                    surveys => new SurveyResponseDto()
                    {
                        IsDeleted = surveys.IsDeleted,
                        Url = surveys.IsDeleted ? "You cant get the url the survey is deleted restore the survey to see the url " : surveys.Url,
                        Id = surveys.Id,
                        Name = surveys.Name,
                        Description = surveys.Description,
                        Questions = surveys.SurveysXQuestions.Select(surveyxquestion => new QuestionResponseDto()
                        {
                            Id = surveyxquestion.Question.Id,
                            Name = surveyxquestion.Question.Name,
                            Title = surveyxquestion.Question.Title,
                            IsRequired = surveyxquestion.Question.IsRequired,
                            Type = surveyxquestion.Question.Type.ToString(),
                        }
                        ).ToList()
                    }
                    ).ToList();
                }
                return Surveys;
            }
            catch (Exception)
            {
                return null;
            }
        }


        /// <summary>
        /// Find Survey By Id
        /// </summary>
        /// <param name="Survey"></param>
        /// <returns></returns>
        public List<SurveyResponseDto> GetSurveyById(int Id)
        {
            try
            {
                List<SurveyResponseDto> Surveys;

                using (var db = new SurveyContext(_configuration))
                {
                    Surveys = (from q in db.Set<Survey>() select q)
                    .Where(q => q.Id == Id)
                    .Include(q => q.SurveysXQuestions)
                    .ThenInclude(q => q.Question)
                    .Select(
                    surveys => new SurveyResponseDto()
                    {
                        IsDeleted = surveys.IsDeleted,
                        Url = surveys.IsDeleted ? "You cant get the url the survey is deleted restore the survey to see the url " : surveys.Url,
                        Id = surveys.Id,
                        Name = surveys.Name,
                        Description = surveys.Description,
                        Questions = surveys.SurveysXQuestions.Select(surveyxquestion => new QuestionResponseDto()
                        {
                            Id = surveyxquestion.Question.Id,
                            Name = surveyxquestion.Question.Name,
                            Title = surveyxquestion.Question.Title,
                            IsRequired = surveyxquestion.Question.IsRequired,
                            Type = surveyxquestion.Question.Type.ToString(),
                        }
                        ).ToList()
                    }
                    ).ToList();
                }
                return Surveys;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Get all Survey 
        /// </summary>
        /// <param name="Survey"></param>
        /// <returns></returns>
        public List<SurveyResponseDto> GetSurveys()
        {
            try
            {
                List<SurveyResponseDto> Surveys;

                using (var db = new SurveyContext(_configuration))
                { 
                    Surveys =(from q in db.Set<Survey>() select q)
                    .Include(q=> q.SurveysXQuestions)
                    .ThenInclude(q=> q.Question)
                    .Select(
                    surveys => new SurveyResponseDto()
                    {
                        IsDeleted = surveys.IsDeleted,
                        Url = surveys.IsDeleted ? "You cant get the url the survey is deleted restore the survey to see the url " : surveys.Url,
                        Id = surveys.Id,
                        Name = surveys.Name,
                        Description = surveys.Description,
                        Questions = surveys.SurveysXQuestions.Select( surveyxquestion => new QuestionResponseDto() 
                        {
                            Id= surveyxquestion.Question.Id,
                            Name = surveyxquestion.Question.Name,
                            Title = surveyxquestion.Question.Title,
                            IsRequired = surveyxquestion.Question.IsRequired, 
                            Type = surveyxquestion.Question.Type.ToString(),
                        }
                        ).ToList()
                    }
                    ).ToList();
                }
                return Surveys;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Create a Survey 
        /// </summary>
        /// <param name="Survey"></param>
        /// <returns></returns>
        public string CreateSurvey(SurveyRequestDto Survey, string Url) 
        {
            using (var db = new SurveyContext(_configuration))
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        //Verify if the survey exists, if exists, Rollback and return survey already exists
                        var FindSurvey = (from q in db.Set<Survey>() select q)
                                      .Where(q => q.Name == Survey.Name)
                                      .FirstOrDefault();
                        if (FindSurvey != null)
                        {
                            dbContextTransaction.Rollback();
                            return "Survey with that name already exists";
                        }
                        var newSurvey = new Survey()
                        {
                            Name = Survey.Name,
                            Description = Survey.Description,
                            IsDeleted = false
                        };
                        db.Set<Survey>().Add(newSurvey);
                        db.SaveChanges();
                        //Set the Url for access to the survey
                        newSurvey.Url = $"https://{Url}/Poll/{newSurvey.Id}";
                        db.Entry(newSurvey).State = EntityState.Modified;
                        db.SaveChanges();
                        var SurveyId = newSurvey.Id;


                        //Verify if the question exists, if exist get the Id, if not Create the question, finally join to the survey
                        foreach (var question in Survey.Questions)
                        {
                            var FindQuestionId = (from q in db.Set<Question>() select q)
                                          .Where(q=> q.Name == question.Name).FirstOrDefault();
                            if (FindQuestionId == null)
                            {
                                var newQuestion = new Question()
                                {
                                    Name = question.Name,
                                    Title = question.Title,
                                    IsRequired = question.IsRequired,
                                    Type = question.Type
                                };
                                db.Set<Question>().Add(newQuestion);
                                db.SaveChanges();
                                question.Id = newQuestion.Id;
                            }
                            else 
                            {
                                question.Id = FindQuestionId.Id;
                            }
                            var newSurveyXQuestion = new SurveyXQuestion() 
                            {
                                SurveyId = SurveyId,
                                QuestionId = (int)question.Id
                            };
                            db.Set<SurveyXQuestion>().Add(newSurveyXQuestion);
                            db.SaveChanges();
                        }
                        dbContextTransaction.Commit();
                        return newSurvey.Url;
                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback();
                        return $"Error {ex.Message}";
                    }
                }
            }
        }


        /// <summary>
        /// Update a Survey 
        /// </summary>
        /// <param name="Survey"></param>
        /// <returns></returns>
        public string UpdateSurvey(int Id, SurveyRequestDto Survey)
        {
            using (var db = new SurveyContext(_configuration))
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        //Verify if the survey exists but the id not match, if  not, return survey not match
                        var FindSurvey = (from q in db.Set<Survey>() select q)
                                      .Where(q => q.Id != Id && q.Name == Survey.Name)
                                      .FirstOrDefault();

                        if (FindSurvey != null)
                        {
                            return "Error The id not match please verify and try again";
                        }
                        //Verify if the survey exists, if  not exists, return survey not exists
                        FindSurvey = (from q in db.Set<Survey>() select q)
                                      .Where(q => q.Id == Id && q.Name == Survey.Name)
                                      .FirstOrDefault();
                        if (FindSurvey == null)
                        {
                            return "Error Survey not exists, first create the survey if you wanna to update it";
                        }
                        FindSurvey.Name = Survey.Name;
                        FindSurvey.Description = Survey.Description;

                        db.Entry(FindSurvey).State = EntityState.Modified;
                        db.SaveChanges();

                        //Verify if the question exists, if exist update it, if not Create the question, finally join to the survey
                        foreach (var question in Survey.Questions)
                        {


                            var FindIfNameExists = (from q in db.Set<Question>() select q)
                                                .Where(q => q.Name == question.Name).FirstOrDefault();
                            if (FindIfNameExists == null)
                            {
                                var Question = (from q in db.Set<Question>() select q)
                                                    .Where(q => q.Id == question.Id).First();
                                Question.Name = question.Name;
                                Question.Title = question.Title;
                                Question.IsRequired = question.IsRequired;
                                Question.Type = question.Type;
                                db.Entry(Question).State = EntityState.Modified;
                                db.SaveChanges();
                                continue;
                            }
                            //If exists the Name of the question in another row, change the Id of the SurveyXQuestion Table
                            var SurveyXQuestion = (from q in db.Set<SurveyXQuestion>() select q)
                                                  .Where(q => q.QuestionId == question.Id && q.SurveyId == FindSurvey.Id).First();
                            var FindQuestion = (from q in db.Set<Question>() select q)
                                                .Where(q =>q.Id != question.Id && q.Name == question.Name).First();
                            if (FindQuestion != null) 
                            {
                                SurveyXQuestion.QuestionId = FindQuestion.Id;
                                db.Entry(SurveyXQuestion).State = EntityState.Modified;
                                db.SaveChanges();
                                continue;
                            }
                            else if(question.Id == null)
                            {
                                var newQuestion = new Question()
                                {
                                    Name = question.Name,
                                    Title = question.Title,
                                    IsRequired = question.IsRequired,
                                    Type = question.Type
                                };
                                db.Set<Question>().Add(newQuestion);
                                db.SaveChanges();
                                question.Id = newQuestion.Id;
                                var newSurveyXQuestion = new SurveyXQuestion()
                                {
                                    SurveyId = FindSurvey.Id,
                                    QuestionId = (int)question.Id
                                };
                                db.Set<SurveyXQuestion>().Add(newSurveyXQuestion);
                                db.SaveChanges();
                            }
                        }
                        dbContextTransaction.Commit();
                        return "200";
                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback();
                        return $"Error {ex.Message}";
                    }
                }
            }
        }

        /// <summary>
        /// Delete a Survey 
        /// </summary>
        /// <param name="Survey"></param>
        /// <returns></returns>
        public string DeleteSurvey(int Id)
        {
            using (var db = new SurveyContext(_configuration))
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        //Verify if the survey exists, if  not exists, return survey not exists
                        var FindSurvey = (from q in db.Set<Survey>() select q)
                                      .Where(q => q.Id == Id)
                                      .FirstOrDefault();
                        if (FindSurvey == null)
                        {
                            return "Error Survey not exists";
                        }
                        FindSurvey.IsDeleted = true;
                        db.Entry(FindSurvey).State = EntityState.Modified;
                        db.SaveChanges();
                        dbContextTransaction.Commit();
                        return "200";
                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback();
                        return $"Error {ex.Message}";
                    }
                }
            }
        }

        /// <summary>
        /// restore a Survey 
        /// </summary>
        /// <param name="Survey"></param>
        /// <returns></returns>
        public string RestoreSurvey(int Id)
        {
            using (var db = new SurveyContext(_configuration))
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        //Verify if the survey exists, if  not exists, return survey not exists
                        var FindSurvey = (from q in db.Set<Survey>() select q)
                                      .Where(q => q.Id == Id)
                                      .FirstOrDefault();
                        if (FindSurvey == null)
                        {
                            return "Error Survey not exists";
                        }
                        FindSurvey.IsDeleted = false;
                        db.Entry(FindSurvey).State = EntityState.Modified;
                        db.SaveChanges();
                        dbContextTransaction.Commit();
                        return "200";
                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback();
                        return $"Error {ex.Message}";
                    }
                }
            }
        }
    }
}
