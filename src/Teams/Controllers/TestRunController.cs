using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Teams.Data;
using Teams.Data.TestRunRepos;
using Teams.Domain;
using Teams.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Teams.Controllers
{
    public class TestRunController : Controller
    {
        private ITestRunRepository _testRunRepository;
        private ApplicationDbContext _applicationDbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private ApplicationUser _applicationUser;

        public TestRunController(ITestRunRepository testRunRepository,
            ApplicationDbContext applicationDbContext, UserManager<ApplicationUser> userManager)
        {
            _testRunRepository = testRunRepository;
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            _applicationUser = await _userManager.GetUserAsync(User);
            return View(await _testRunRepository.GetAllAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Run(Guid testRunId)
        {
            var testRun = await _testRunRepository.GetByIdAsync(testRunId);

            if (testRun == null) return NotFound();

            var testQuestionIds =
                _applicationDbContext.TestQuestions.Where(q => q.TestId == testRun.TestId).Select(x => x.Id).ToList();

            var answers =
                AnswerDTOConvert(
                    _applicationDbContext.Answers.Where(x => testQuestionIds.Contains(x.TestQuestionId)).ToList(),
                    testRun);
            var testRunDto = new TestRunDTO(answers,
                _applicationDbContext.TestQuestions.Where(x => testQuestionIds.Contains(x.Id)).ToList(),
                testRun.TestId);
            return View(testRunDto);
        }

        [HttpPost]
        public async Task<IActionResult> Run(TestRunDTO testRunDto)
        {
            var updatedTestRun = await _testRunRepository.GetByIdAsync(testRunDto.Id);
            if (updatedTestRun == null) return NotFound();
            // foreach (var answer in testRunDto.Answers)
            //         updatedTestRun.Add(new Answer(answer.Answers, answer.TestQuestionId, answer.Id));
            updatedTestRun.Finish();
            await SaveTestRun(updatedTestRun);
            return View(testRunDto);
        }


        [HttpPost]
        public async Task<IActionResult> AddAnswer(AnswerDTO answerDto)
        {
            if (answerDto == null) return NotFound();
            // Answer answer = new Answer(answerDto.Answers, answerDto.TestQuestionId, answerDto.Id);
            // await SaveAnswer(answer);
            return View();
        }

        private async Task<bool> SaveTestRun(TestRun testRun)
        {
            try
            {
                _applicationDbContext.TestRuns.Update(testRun);
                await _applicationDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        
        private async Task<bool> SaveAnswer(Answer answer)
        {
            try
            {
                _applicationDbContext.Answers.Update(answer);
                await _applicationDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private List<AnswerDTO> AnswerDTOConvert(List<Answer> answers, TestRun testRun)
        {
            var answerDTO = new List<AnswerDTO>();
            // foreach (var answer in answers)
            //     answerDTO.Add
            //     (
            //         new AnswerDTO(answer.Answers, answer.Id, testRun.Id, answer.TestQuestionId));

            return answerDTO;
        }
    }
}