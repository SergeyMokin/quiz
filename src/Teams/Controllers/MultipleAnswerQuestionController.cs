﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Teams.Data;
using Teams.Data.Repositories;
using Teams.Domain;
using Teams.Models;

namespace Teams.Controllers
{
    public class MultipleAnswerQuestionController : Controller
    {
        public MultipleAnswerQuestionController(IMultipleAnswerQuestionRepository questionRepository)
        {
            this.questionRepository = questionRepository;
        }
        private readonly IMultipleAnswerQuestionRepository questionRepository;
        [HttpGet]
        public IActionResult Index(Guid id)
        {
            var question = new MultipleAnswerQuestionViewModel()
            {
                SourceQuestion = questionRepository.PickById(id)
            };
            return View("MultipleAnswerQuestionForm", question);
        }
        public IActionResult MultipleAnswerQuestionForm(string id, int[] answers)
        {
            var question = new MultipleAnswerQuestionViewModel()
            {
                SourceQuestion = questionRepository.PickById(new Guid(id)),
                ChosenOptions = answers
            };
            return View(question);
        }

        public IActionResult EditMultipleAnswerQuestion(Guid id)
        {
            var question = new MultipleAnswerQuestionViewModel()
            {
                SourceQuestion = questionRepository.PickById(id),
                // ChosenOptions = answers
            };
            return View(question);
        }

        public IActionResult AddMultipleAnswerQuestion()
        {
            return View();
        }

        public IActionResult ForCheck(string questionText)
        {
            ViewBag.Text = "questionText";
            return View();
        }

        [HttpPost]
        public IActionResult AddMultipleAnswerQuestion([FromBody] MAQDTOModel fromAjax)
        {
            //MultipleAnswerQuestion question = new MultipleAnswerQuestion(questionText, answers);
            //questionRepository.MethodForAdd(question);
            string text;
            if(fromAjax.jTextList == null)
            {
                 text = "yes";
            }
            else
            {
                text = "no";
            }
            return View("ForCheck", text); 
            
        }
        [HttpPost]
        public IActionResult EditMultipleAnswerQuestion(List<MultipleAnswerQuestionOption> editAnswers, string questionText, string Id)
        {
            
            return Content(questionText);
        }

    }
}
