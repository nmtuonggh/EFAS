public class FactoryStates 
{
   StatesMachineController _context;
   
   public FactoryStates(StatesMachineController currentContext)
   {
       _context = currentContext;
   }

   public PlayerBaseState Idle()
   {
       return new IdleState(_context, this);
   }
   public PlayerBaseState Jump(){       
       return new JumpState(_context, this);
   }
   public PlayerBaseState Walk(){       
       return new WalkState(_context, this);
   }
   public PlayerBaseState Run(){       
       return new RunState(_context, this);
   }
   public PlayerBaseState Fall(){       
       return new FallState(_context, this);
   }
}
