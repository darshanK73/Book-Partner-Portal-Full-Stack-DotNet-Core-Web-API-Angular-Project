import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AuthorRegisterComponent } from './author-register.component';

describe('AuthorRegisterComponent', () => {
  let component: AuthorRegisterComponent;
  let fixture: ComponentFixture<AuthorRegisterComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AuthorRegisterComponent]
    });
    fixture = TestBed.createComponent(AuthorRegisterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
