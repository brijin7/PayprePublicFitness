
import { GalleryDescription } from '../Home/GalleryDescription.js'
import { ClassesHome } from '../Home/Homeclasses.js'
import { Testimonials } from '../Home/Testimonials.js'
import { Subscription } from '../Home/Home_Subscription.js'
import { TrainersNameHome } from '../Home/TrainersName.js'
//import { TrainerDescription } from '../Home/GalleryTrainer.js'
//import { Trainersdetails } from '../Home/Trainer_Details.js'


window.onload = function () {
    Subscription();
    GalleryDescription();
    ClassesHome();
    Testimonials();
    TrainersNameHome();
    //TrainerDescription();
    //Trainersdetails();
}

