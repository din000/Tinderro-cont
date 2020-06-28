import { CanDeactivate } from '@angular/router';
import { UserEditComponent } from '../user-edit/user-edit.component';

export class PreventUnsaveChanges implements CanDeactivate<UserEditComponent> {
    canDeactivate(component: UserEditComponent) {
        if (component.editform.dirty) {
            return confirm('Zmiany zostana porzucone, jestes pewien?');
        }
        return true;
    }
}
