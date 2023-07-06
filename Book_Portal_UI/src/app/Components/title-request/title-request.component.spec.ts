import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TitleRequestComponent } from './title-request.component';

describe('TitleRequestComponent', () => {
  let component: TitleRequestComponent;
  let fixture: ComponentFixture<TitleRequestComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [TitleRequestComponent]
    });
    fixture = TestBed.createComponent(TitleRequestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
