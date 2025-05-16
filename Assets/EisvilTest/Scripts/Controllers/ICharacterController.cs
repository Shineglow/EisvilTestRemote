using EisvilTest.Scripts.Input;

namespace EisvilTest.Scripts.Controllers
{
    public interface ICharacterController
    {
        InputAbstractionSetter InputAbstraction { get; set; }
    }
}