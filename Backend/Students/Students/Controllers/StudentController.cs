using Microsoft.AspNetCore.Mvc;
using Students.DAL.StudentRepository;
using Students.Domain;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Students.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;
        public StudentController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }


        // GET: api/<ValuesController>
        [HttpGet]
        public ActionResult Get()
        {
            var result = _studentRepository.Read();
            return Ok(result);
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public ActionResult Get(Guid id)
        {
            var result = _studentRepository.Read(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public ActionResult Post(Student student)
        {
            _studentRepository.Create(student);
            return Ok();
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public ActionResult Put(Guid id, Student student)
        {
            var result = _studentRepository.Update(id, student);
            if (result)
                return Ok();
            return NotFound();
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            var result = _studentRepository.Delete(id);
            if (result)
                return Ok();
            return NotFound();
        }
    }
}
