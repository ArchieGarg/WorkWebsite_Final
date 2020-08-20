using Login_Form.Models;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using MySqlX.XDevAPI;
using System.CodeDom.Compiler;

namespace Login_Form.Controllers
{
    public class NewHomeController : NewParentController
    {
        private static NewStoreItem[] allItems = {

            new NewStoreItem{name="Carrot",quantity=12,cost=2.00,description="What Bunnies Like to Eat",image="images/carrot.jfif", 
                detailedDescription="The carrot (Daucus carota subsp. sativus) is a root vegetable, usually orange in colour, though purple, black, red, white, and yellow cultivars exist.[2] They are a domesticated form of the wild carrot, Daucus carota, native to Europe and Southwestern Asia. The plant probably originated in Persia and was originally cultivated for its leaves and seeds. The most commonly eaten part of the plant is the taproot, although the stems and leaves are eaten as well. The domestic carrot has been selectively bred for its greatly enlarged, more palatable, less woody-textured taproot.",
                Fact1="Are Orange",
                Fact2 = "Come from the ground",
                Fact3 = "Have no flavor"},
            new NewStoreItem{name="Potato",quantity=6,cost=3.00,description="What Makes You Fat",image="images/potato.png", 
                detailedDescription="The potato is a root vegetable native to the Americas, a starchy tuber of the plant Solanum tuberosum, and the plant itself is a perennial in the nightshade family, Solanaceae.[2] Wild potato species, originating in modern-day Peru, can be found throughout the Americas, from the United States to southern Chile.[3] The potato was originally believed to have been domesticated by indigenous peoples of the Americas independently in multiple locations,[4] but later genetic testing of the wide variety of cultivars and wild species traced a single origin for potatoes. In the area of present-day southern Peru and extreme northwestern Bolivia, from a species in the Solanum brevicaule complex, potatoes were domesticated approximately 7,000–10,000 years ago.[5][6][7] In the Andes region of South America, where the species is indigenous, some close relatives of the potato are cultivated.",
                Fact1="Come from the ground",
                Fact2 = "Skin Tastes Good",
                Fact3 = "Unhealthy if eaten for prolonged periods."},
            new NewStoreItem{name="Apple",quantity=24,cost=4.00,description="What Archie Likes to Eat",image="images/apple.png", 
                detailedDescription="An apple is an edible fruit produced by an apple tree (Malus domestica). Apple trees are cultivated worldwide and are the most widely grown species in the genus Malus. The tree originated in Central Asia, where its wild ancestor, Malus sieversii, is still found today. Apples have been grown for thousands of years in Asia and Europe and were brought to North America by European colonists. Apples have religious and mythological significance in many cultures, including Norse, Greek and European Christian tradition.",
                Fact1="Are red",
                Fact2 = "Are Tasty",
                Fact3 = "Seeds are poison though" },
            new NewStoreItem{name="Tomato",quantity=8,cost=1.00,description="What Aadi Doesn't Like to Eat",image="images/tomato.png",
                detailedDescription="The tomato is the edible, often red berry of the plant Solanum lycopersicum,[2][1] commonly known as a tomato plant. The species originated in western South America and Central America.[2][3] The Nahuatl (the language used by the Aztecs) word tomatl gave rise to the Spanish word tomate, from which the English word tomato derived.[3][4] Its domestication and use as a cultivated food may have originated with the indigenous peoples of Mexico.[2][5] The Aztecs used tomatoes in their cooking at the time of the Spanish conquest of the Aztec Empire, and after the Spanish encountered the tomato for the first time after their contact with the Aztecs, they brought the plant to Europe. From there, the tomato was introduced to other parts of the European-colonized world during the 16th century.[2]",
                Fact1="Sweet",
                Fact2 = "Has lots of seeds",
                Fact3 = "Soft skin, easy to cut"},
            new NewStoreItem{name="Orange",quantity=36,cost=10.00,description="What Archie Loves to Eat",image="images/orange.jfif",
                detailedDescription="The orange is the fruit of various citrus species in the family Rutaceae (see list of plants known as orange); it primarily refers to Citrus × sinensis,[1] which is also called sweet orange, to distinguish it from the related Citrus × aurantium, referred to as bitter orange. The sweet orange reproduces asexually (apomixis through nucellar embryony); varieties of sweet orange arise through mutations.[2][3][4][5]. The orange is a hybrid between pomelo (Citrus maxima) and mandarin (Citrus reticulata).[2][6] The chloroplast genome, and therefore the maternal line, is that of pomelo.[7] The sweet orange has had its full genome sequenced.[2]The orange originated in a region comprising Southern China, Northeast India, and Myanmar,[8][9] and the earliest mention of the sweet orange was in Chinese literature in 314 BC.[2] As of 1987, orange trees were found to be the most cultivated fruit tree in the world.[10] Orange trees are widely grown in tropical and subtropical climates for their sweet fruit. The fruit of the orange tree can be eaten fresh, or processed for its juice or fragrant peel.[11] As of 2012, sweet oranges accounted for approximately 70% of citrus production.[12]",
                Fact1="Seet Also",
                Fact2 = "Difficult to peel",
                Fact3 = "Are good during the summer but come only in the winter"},
            new NewStoreItem{name="Broccoli",quantity=3,cost=0.50,description="What Nobody Likes to Eat",image="images/broccoli.jfif",
                detailedDescription="Broccoli is an edible green plant in the cabbage family (family Brassicaceae, genus Brassica) whose large flowering head and stalk is eaten as a vegetable. The word broccoli comes from the Italian plural of broccolo, which means \"the flowering crest of a cabbage\", and is the diminutive form of brocco, meaning \"small nail\" or \"sprout\".[3] Broccoli is classified in the Italica cultivar group of the species Brassica oleracea. Broccoli has large flower heads, usually dark green in color, arranged in a tree-like structure branching out from a thick stalk which is usually light green. The mass of flower heads is surrounded by leaves. Broccoli resembles cauliflower, which is a different cultivar group of the same Brassica species. In 2017, China and India combined produced 73% of the world's broccoli and cauliflower crops.[4]. Broccoli resulted from breeding of cultivated Brassica crops in the northern Mediterranean starting in about the sixth century BC.[5] Broccoli has its origins in primitive cultivars grown in the Roman Empire.[6] It is eaten raw or cooked. Broccoli is a particularly rich source of vitamin C and vitamin K. Contents of its characteristic sulfur-containing glucosinolate compounds, isothiocyanates and sulforaphane, are diminished by boiling, but are better preserved by steaming, microwaving or stir-frying.[7]",
                Fact1="Bad",
                Fact2 = "Bland",
                Fact3 = "Nobody Eats Broccoli"},
            new NewStoreItem{name="Lettuce",quantity=12,cost=2.50,description="What Is Neccesary for Quesidillas",image="images/lettuce.jfif",
                detailedDescription="Lettuce (Lactuca sativa) is an annual plant of the daisy family, Asteraceae. It is most often grown as a leaf vegetable, but sometimes for its stem and seeds. Lettuce is most often used for salads, although it is also seen in other kinds of food, such as soups, sandwiches and wraps; it can also be grilled.[3] One variety, the woju (t:萵苣/s:莴苣), or asparagus lettuce (Celtuce), is grown for its stems, which are eaten either raw or cooked. In addition to its main use as a leafy green, it has also gathered religious and medicinal significance over centuries of human consumption. Europe and North America originally dominated the market for lettuce, but by the late 20th century the consumption of lettuce had spread throughout the world. World production of lettuce and chicory for calendar year 2017 was 27 million tonnes, 56% of which came from China.[4]. Lettuce was originally farmed by the ancient Egyptians, who transformed it from a weed whose seeds were used to create oil into an important food crop raised for its succulent leaves and oil-rich seeds. Lettuce spread to the Greeks and Romans, the latter of whom gave it the name lactuca, from which the English lettuce is ultimately derived. By 50 AD, many types were described, and lettuce appeared often in medieval writings, including several herbals. The 16th through 18th centuries saw the development of many varieties in Europe, and by the mid-18th century cultivars were described that can still be found in gardens.",
                Fact1="Green",
                Fact2 = "Great for Quessadillas and salads",
                Fact3 = "Very Healthy" },
            new NewStoreItem{name="Olive",quantity=17,cost=1.00,description="What goes well with pasta",image="images/olive.jpg",
                detailedDescription="The olive, known by the botanical name Olea europaea, meaning \"European olive\", is a species of small tree in the family Oleaceae, found traditionally in the Mediterranean Basin. The species is cultivated in all the countries of the Mediterranean, as well as in South America, South Africa, Australia, New Zealand and the United States.[1][2] Olea europaea is the type species for the genus Olea. The olive's fruit, also called the olive, is of major agricultural importance in the Mediterranean region as the source of olive oil; it is one of the core ingredients in Mediterranean cuisine. The tree and its fruit give their name to the plant family, which also includes species such as lilacs, jasmine, Forsythia, and the true ash trees (Fraxinus).",
                Fact1="Used to make wine",
                Fact2 = "Can be sour",
                Fact3 = "Came from greece"},
            new NewStoreItem{name="Pizza",quantity=100,cost=4.00,description="The Best Fruit of All",image="images/Pizza.jfif",
                detailedDescription="Pizza (Italian: [ˈpittsa], Neapolitan: [ˈpittsə]) is a savory dish of Italian origin consisting of a usually round, flattened base of leavened wheat-based dough topped with tomatoes, cheese, and often various other ingredients (such as anchovies, mushrooms, onions, olives, pineapple, meat, etc.) which is then baked at a high temperature, traditionally in a wood-fired oven.[1] A small pizza is sometimes called a pizzetta. A person who makes pizza is known as a pizzaiolo. In Italy, pizza served in formal settings, such as at a restaurant, is presented unsliced, and is eaten with the use of a knife and fork.[2][3] In casual settings, however, it is cut into wedges to be eaten while held in the hand. The term pizza was first recorded in the 10th century in a Latin manuscript from the Southern Italian town of Gaeta in Lazio, on the border with Campania.[4] Modern pizza was invented in Naples, and the dish and its variants have since become popular in many countries.[5] It has become one of the most popular foods in the world and a common fast food item in Europe and North America, available at pizzerias (restaurants specializing in pizza), restaurants offering Mediterranean cuisine, and via pizza delivery.[5][6] Many companies sell ready-baked frozen pizzas to be reheated in an ordinary home oven.",
                Fact1="The sauce is the best part?",
                Fact2 = "Can be very cheessy also?",
                Fact3 = "Came from Italy most probably."},
            new NewStoreItem{name="Lasangna",quantity=1,cost=10.00,description="The King of All Fruit",image="images/lasangna.jpg",
                detailedDescription="Lasagne originated in Italy during the Middle Ages and have traditionally been ascribed to the city of Naples. The first recorded recipe was set down in the early 14th-century Liber de Coquina (The Book of Cookery).[3] It bore only a slight resemblance to the later traditional form of lasagne, featuring a fermented dough flattened into thin sheets (lasagne), boiled, sprinkled with cheese and spices, and then eaten with a small pointed stick.[4] Recipes written in the century following the Liber de Coquina recommended boiling the pasta in chicken broth and dressing it with cheese and chicken fat. In a recipe adapted for the Lenten fast, walnuts were recommended.[4] The traditional lasagne of Naples, lasagne di carnevale, are layered with local sausage, small fried meatballs, hard-boiled eggs, ricotta and mozzarella cheeses, and sauced with a Neapolitan ragù, a meat sauce.[5] Lasagne al forno, layered with a thicker ragù and Béchamel sauce, and corresponding to the most common version of the dish outside Italy, are traditionally associated with the Emilia-Romagna region of Italy. In other regions, lasagne can be made with various combinations of ricotta or mozzarella cheese, tomato sauce, meats (e.g., ground beef, pork or chicken), and vegetables (e.g., spinach, zucchini, olives, mushrooms), and the dish is typically flavoured with wine, garlic, onion, and oregano. In all cases, the lasagne are oven-baked (al forno).",
                Fact1="My Favorite Piece of Food",
                Fact2 = "The kpp of all food, because of how many layers --that is a reference by the way",
                Fact3 = "Tastes so so so good, I could die just by eating it."},
            new NewStoreItem{name="Quesadilla",quantity=2,cost=9.00,description="The President of All Kinds of Fruit",image="images/quesadilla.jfif",
                detailedDescription="In the central and southern regions of Mexico, a quesadilla is a flat circle of cooked corn masa, called a tortilla, warmed to soften it enough to be folded in half, and then filled. They are typically filled with Oaxaca cheese (queso Oaxaca), a stringy Mexican cheese made by the pasta filata (stretched-curd) method. The quesadilla is then cooked on a comal until the cheese has completely melted. They are usually cooked without the addition of any oil. Often the quesadillas are served with green or red salsa, chopped onion, and guacamole.[5] While Oaxaca (or string) cheese is the most common filling, other ingredients are also used in addition to, or even substituting for, the cheese. These can include cooked vegetables, such as potatoes with chorizo, squash blossoms, mushrooms, epazote, huitlacoche, and different types of cooked meat, such as chicharron, tinga made of chicken or beef, or cooked pork. In some places, quesadillas are also topped with other ingredients, in addition to the fillings they already have. Avocado or guacamole, chopped onion, tomato, serrano chiles, and cilantro are the most common. Salsas may also be added as a topping.[6]",
                Fact1="Works well with lettuce",
                Fact2 = "Came from Mexico",
                Fact3 = "Perfect party food, you will have a blast, when you eat it."},
            new NewStoreItem{name="Pancakes",quantity=25,cost=2.50,description="The Prime Minister of All Kinds of Fruit",image="images/pancakes.jfif",
                detailedDescription="A pancake (or hotcake, griddlecake, or flapjack, not to be confused with oat bar flapjacks) is a flat cake, often thin and round, prepared from a starch-based batter that may contain eggs, milk and butter and cooked on a hot surface such as a griddle or frying pan, often frying with oil or butter. Archaeological evidence suggests that pancakes were probably the earliest and most widespread cereal food eaten in prehistoric societies.[1] The pancake's shape and structure varies worldwide. In England, pancakes are often unleavened and resemble a crêpe.[2] In North America, a leavening agent is used (typically baking powder) creating a thick fluffy pancake. A crêpe is a thin Breton pancake of French origin cooked on one or both sides in a special pan or crepe maker to achieve a lacelike network of fine bubbles. A well-known variation originating from southeast Europe is a palačinke, a thin moist pancake fried on both sides and filled with jam, cream cheese, chocolate, or ground walnuts, but many other fillings—sweet or savoury—can also be used. When potato is used as a major portion of the batter, the result is a potato pancake. Commercially prepared pancake mixes are available in some countries. When buttermilk is used in place of or in addition to milk, the pancake develops a tart flavor and becomes known as a buttermilk pancake, which is common in Scotland and the US. Buckwheat flour can be used in a pancake batter, making for a type of buckwheat pancake, a category that includes Blini, Kaletez, Ploye, and Memil-buchimgae.",
                Fact1="Works well with syrup",
                Fact2 = "Are sweet by themselves sometimes",
                Fact3 = "Perfect Breakfast food"}
        };

        [HttpPost]
        public String PostAddItemToUserCart(String user, String item, int quant)
        {
            //look through all items and negate accrodingly on instance var
            //add item to user cart
            //on front end, update cart somehow to show the number of UNIQUE items they have added
            //if(item.quantity-quant < 0) return "Bought To Much"

            List<NewStoreItem> userCart = GetUserCart(user);
            NewStoreItem currentItem_ = null;
            foreach (NewStoreItem currentItem in allItems)
            {
                if (String.Equals(item, currentItem.name))
                {
                    currentItem_ = currentItem;
                }
            }

            Debug.WriteLine("Item: {0} w/ quant: {1}", item, currentItem_.quantity);
            if (currentItem_.quantity == 0)
            {
                Debug.WriteLine("store is out but maybe possible to take from stacks");
                if (currentItem_.showQuantity < quant)
                    return "Bought Too Much!";
                return standardProcedure(item, userCart, quant, currentItem_);
            }
            else
            {
                if (currentItem_.quantity - quant >= 0)
                {
                    currentItem_.quantity -= quant;

                    int quantAlready = 0;
                    NewStoreItem currItem_ = null;
                    foreach (NewStoreItem currItem in userCart)
                    {
                        if (currItem.name.Equals(item))
                        {
                            quantAlready += currItem.quantity;
                            currItem_ = currItem;
                        }
                    }

                    if (quantAlready == 0)
                        userCart.Add(new NewStoreItem { name = item, quantity = quant, showQuantity = 0, cost = GetCostOfItem(item) * quant, description = "", image = "" });
                    else
                    {
                        currItem_.quantity += quant;
                        currItem_.cost = GetCostOfItem(item) * currItem_.quantity;
                    }
                    return "Successfully Added to Cart";
                }
                else
                {
                    return standardProcedure(item, userCart, quant, currentItem_);
                }
            }
        }

        private String standardProcedure(string item, List<NewStoreItem> userCart, int quant, NewStoreItem currentItem)
        {
            Debug.WriteLine("in");
            int quantToLookFor = quant - currentItem.quantity;
            int quantTotalInEachStack = 0;
            Debug.WriteLine("quant to look for: " + quantToLookFor);
            foreach (NewUser currentUser in GetUsers())
            {
                foreach (NewStoreItem currentUsersItem in currentUser.GetStack())
                {

                    if (String.Equals(item, currentUsersItem.name))
                    {
                        quantTotalInEachStack += currentUsersItem.quantity;
                    }
                }
            }
            Debug.WriteLine("quant total in stack: " + quantTotalInEachStack);

            if (quantTotalInEachStack - quantToLookFor < 0)
                return "Bought Too Much!";

            Debug.WriteLine("have enough to take from user's stack");
            foreach (NewUser currentUser in GetUsers())
            {

                foreach (NewStoreItem currentUsersItem in currentUser.GetStack())
                {
                    Debug.WriteLine("item: " + currentUsersItem.name);
                    if (String.Equals(item, currentUsersItem.name))
                    {
                        Debug.WriteLine("quant: " + currentUsersItem.quantity);
                        if (currentUsersItem.quantity == quantToLookFor)
                        {
                            Debug.WriteLine("in quant to look for");
                            currentUsersItem.quantity = 0;
                            currentItem.quantity = 0;
                            currentItem.showQuantity = 0;
                            RemoveItemFromUserDeletions(currentUser, item);
                            userCart.Add(new NewStoreItem { name = currentUsersItem.name, quantity = quant, showQuantity = 0, cost = GetCostOfItem(item) * quant, description = "", image = "" });
                            return "Successfully Added to Cart";
                        }
                        else
                        {
                            Debug.WriteLine("in other");
                            quant -= currentItem.quantity;
                            quant -= currentUsersItem.quantity;
                            currentItem.showQuantity -= currentUsersItem.quantity;
                            currentUsersItem.quantity = 0;
                            RemoveItemFromUserDeletions(currentUser, item);
                            userCart.Add(new NewStoreItem { name = currentUsersItem.name, quantity = quant, showQuantity = 0, cost = GetCostOfItem(item) * quant, description = "", image = "" });
                        }
                        break;
                    }
                }
            }
            return "Successfully Added to Cart";
        }

        private void RemoveItemFromUserDeletions(NewUser user, string item)
        {
            Stack<NewStoreItem> tempStack = new Stack<NewStoreItem>();
            NewStoreItem cursor;
            try
            {
                cursor = user.GetStack().Pop();
            }
            catch (InvalidOperationException e)
            {
                cursor = null;
            }
            while (cursor != null)
            {

                if (!cursor.Equals(item))
                    tempStack.Push(cursor);

                try
                {
                    cursor = user.GetStack().Pop();
                }
                catch (InvalidOperationException e)
                {
                    cursor = null;
                }
            }
            try
            {
                cursor = tempStack.Pop();
            }
            catch (InvalidOperationException e)
            {
                cursor = null;
            }
            while (cursor != null)
            {
                try
                {
                    cursor = tempStack.Pop();
                }
                catch (InvalidOperationException e)
                {
                    cursor = null;
                }
            }
        }

        private double GetCostOfItem(string item)
        {
            foreach (NewStoreItem currentItem in allItems)
            {
                if (String.Equals(item, currentItem.name))
                    return currentItem.cost;
            }
            return 0;
        }

        [HttpPost]
        public void PostEmptyCart(String u)
        {
            List<NewStoreItem> userCart = GetUserCart(u);
            for(int i = 0; i < userCart.Count; i++)
            {
                PostRemoveItemFromCart(u, userCart.ElementAt(i).name);
                i--;
            }

            EmptyUserStack(u);
        }

        private void EmptyUserStack(String u)
        {
            Stack<NewStoreItem> userDeletions = GetUserStack(u);
            try
            {
                while (true)
                {
                    userDeletions.Pop();
                }
            }
            catch (InvalidOperationException e)
            {
                return;
            }
        }

        public List<Int64[]> GetQuantities()
        {

            List<Int64[]> toRet = new List<Int64[]>();
            foreach (NewStoreItem tempStoreItem in allItems)
            {
                Int64[] toAdd = new Int64[2];
                toAdd[0] = tempStoreItem.quantity;
                toAdd[1] = tempStoreItem.showQuantity;
                toRet.Add(toAdd);
            }
            return toRet;
        }

        [HttpGet]
        public IEnumerable<NewStoreItem> GetUserLocalCart(String u)
        {
            return GetUserCart(u);
        }

        [HttpGet]
        public IEnumerable<NewStoreItem> GetAll()
        {
            return allItems;
        }

        [HttpPost]
        public String PostUndoDelete(String u)
        {
            NewStoreItem toAdd = null;
            try
            {
                toAdd = GetUserStack(u).Pop();
            }
            catch (InvalidOperationException e)
            {
                return "Nothing to Undo, Past Deletions Are Empty";
            }
            List<NewStoreItem> userCart = GetUserCart(u);
            userCart.Add(toAdd);
            foreach (NewStoreItem currentItem in allItems)
            {
                if (String.Equals(toAdd.name, currentItem.name))
                {
                    currentItem.showQuantity -= toAdd.quantity;
                }
            }
            return "Item Successfully Added Back!";
        }

        [HttpPost]
        public String PostRemoveItemFromCart(String user, String item)
        {
            Debug.WriteLine("remove from cart: user: " + user + " item: " + item);
            List<NewStoreItem> userCart = GetUserCart(user);
            int quant = 0;
            NewStoreItem itemInQuestion = null;
            foreach (NewStoreItem currentItem in userCart)
            {
                if (String.Equals(item, currentItem.name))
                {
                    Debug.WriteLine("found quantity!");
                    quant = currentItem.quantity;
                    itemInQuestion = currentItem;
                }
            }
            lock (allItems)
            {
                GetUserStack(user).Push(itemInQuestion);
                Debug.WriteLine(userCart.Remove(itemInQuestion));
            }
            foreach (NewStoreItem currentItem in allItems)
            {
                if (String.Equals(item, currentItem.name))
                {
                    Debug.WriteLine("Attempting to Remove item w/ quant {0} and name {1}", quant, item);
                    currentItem.showQuantity += quant;
                    return "Success, removed from cart successfully. It may be recovered, but after a certiain time, the item may be sold.";
                }
            }
            return "";
        }
    }
}
