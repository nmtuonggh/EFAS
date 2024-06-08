public class FactoryStates 
{
   StatesMachineController _context;
   
   public FactoryStates(StatesMachineController currentContext)
   {
       _context = currentContext;
   }

   public IdleState Idle()
   {
       return new IdleState();
   }
   public JumpState Jump(){       
       return new JumpState();
   }
   public WalkState Walk(){       
       return new WalkState();
   }
   public RunState Run(){       
       return new RunState();
   }
   public SlideState Slide(){       
       return new SlideState();
   }
}
