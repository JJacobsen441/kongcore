//using ClientDependency.Core.Logging;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Umbraco.Core;
//using Umbraco.Core.Composing;
//using Umbraco.Core.Services.Implement;

//namespace kongcore.dk.Core.Common
//{
 
//    [RuntimeLevel(MinLevel = RuntimeLevel.Run)]
//    public class LogWhenPublishedComposer : ComponentComposer<LogWhenPublishedComponent>
//    {
//        // nothing needed to be done here!
//    }

//    public class LogWhenPublishedComponent : IComponent
//    {
//        //inject in the core Logger service
//        private readonly ILogger _logger;

//        public LogWhenPublishedComponent(ILogger logger)
//        {
//            _logger = logger;
//        }

//        // initialize: runs once when Umbraco starts
//        public void Initialize()
//        {
//            // subscribe to content service published event
//            ContentService.Published += ContentService_Published;
//        }

//        private void ContentService_Published(Umbraco.Core.Services.IContentService sender, Umbraco.Core.Events.ContentPublishedEventArgs e)
//        {
//            // the custom code to fire everytime content is published goes here!
//            _logger.Info("Something has been published...");
//            foreach (var publishedItem in e.PublishedEntities)
//            {
//                _logger.Info(publishedItem.Name + " was published");
//            }
//        }

//        // terminate: runs once when Umbraco stops
//        public void Terminate()
//        {
//            // unsubscribe on shutdown
//            ContentService.Published -= ContentService_Published;
//        }
//    }
//}