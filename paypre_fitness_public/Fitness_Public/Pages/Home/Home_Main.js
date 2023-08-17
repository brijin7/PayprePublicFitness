import { HomeNavBarClick } from './Home_NavBarClicks.js'
import { Testimonials } from '../Home/Home_Testimonials.js'
import { Subscription } from '../Home/Home_Subscription.js'
import { Classes } from '../Home/Home_Classes.js'
import { onBranchChange } from '../Home/Home_OnBranchChange.js'


window.onload = function () {
    
    onBranchChange();
    HomeNavBarClick();
    Testimonials();
    Subscription();
    Classes();
}