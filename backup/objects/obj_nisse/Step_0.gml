// Finite state machine
if(place_meeting(x,y+1,obj_block)&&state==1) xVel = hDirection*walkingSpeed;

// Falling
if(!place_meeting(x,y+1,obj_block)) {
	yVel += fallAcc;
	if(yVel > maxFallVel) yVel = maxFallVel;
}

// Vertical collision
if(place_meeting(x,y+yVel,obj_block)) {
	while(!place_meeting(x,y+sign(yVel),obj_block))
		y += sign(yVel);
	
	// Create dust on ground when landing
	if(place_meeting(x,y+1,obj_block) && yVel > 0)	instance_create_depth(x-175,y+25,-10,obj_dust);
	if(place_meeting(x,y+1,obj_block) && yVel > 0)	instance_create_depth(x-190,y+25,-10,obj_dust);
	if(place_meeting(x,y+1,obj_block)&& yVel > 0 && random_range(0,2)>1)	instance_create_depth(x-175,y+25,-10,obj_dust);
	if(place_meeting(x,y+1,obj_block)&& yVel > 0 && random_range(0,2)>1)	instance_create_depth(x-175,y+25,-10,obj_dust);
	// Set velocity to zero
	yVel = 0;
}

// Horizontal collision
if(place_meeting(x+xVel,y,obj_block)) {
	while(!place_meeting(x+sign(xVel),y,obj_block))
		x += sign(xVel);
	xVel = 0;
	hDirection *= -1;
}

x += xVel;
y += yVel;

// ------------ Animations

if(yVel != 0) sprite_index = spr_Nisse_Fall;

// Attack
if(yVel == 0 && can_attack && distance_to_object(obj_player)<1000) {
	sprite_index = spr_Nisse_Fire;
	state = 2;
	xVel = 0;
	can_attack = false;
	alarm_set(0,irandom_range(20,100));
}

// Walking
if(yVel == 0 && state == 1) sprite_index = spr_Nisse_Walk;

// Direction
image_xscale = -1 * hDirection;