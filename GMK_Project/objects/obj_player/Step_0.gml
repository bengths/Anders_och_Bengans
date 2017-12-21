/// @description Insert description here

// Get inputs from keyboard
key_right = keyboard_check(vk_right);
key_left = -keyboard_check(vk_left);
key_up = keyboard_check(vk_up);

hDirection = key_right + key_left;

// Check if player is standing on a block
if(place_meeting(x,y+1,obj_block)) {
	// Jumping
		yVel =	- key_up * jumpAcc;
	// Walking
	if(abs(walkSpeed)<maxWalkVel) xVel += hDirection * walkInvFriction;
	
	// Horizontal speed loss
	if(hDirection == 0 && xVel != 0) {
		xVel -= walkInvFriction*walkStopFriction*sign(xVel);
		// Stop oscillations if vel = 0 overidden
		if(abs(xVel)<1.5 && hDirection = 0) xVel = 0;
	}
} else {
	// Falling
	yVel += fallAcc;
	if(yVel > maxFallVel) yVel = maxFallVel;
}


// Vertical collision
if(place_meeting(x,y+yVel,obj_block)) {
	while(!place_meeting(x,y+sign(yVel),obj_block))
		y += sign(yVel);
	yVel = 0;
}

// Horizontal collision
if(place_meeting(x+xVel,y,obj_block)) {
	while(!place_meeting(x+sign(xVel),y,obj_block))
		x += sign(xVel);
	xVel = 0;
}

// Kinematics
y += yVel;
x += xVel;

// ---- Graphics ----
// Idle
if(xVel == 0 && yVel == 0 && place_meeting(x,y+1,obj_block)) { 
	sprite_index = spr_Anton; 
	image_speed = normal_animation_speed;
	}
// Walking
if(xVel != 0 && yVel == 0 && place_meeting(x,y+1,obj_block)) sprite_index = spr_Anton_Walk;
// Falling
if(yVel > 0) {
	sprite_index = spr_Anton_Fall;
	image_index = 1;
	image_speed = 0;
}

if(yVel <= 0 && !place_meeting(x,y+1,obj_block)) {
	sprite_index = spr_Anton_Fall;
	image_index = 0;
	image_speed = 0;
}

// Direction
if(hDirection != 0) lastDirection = hDirection;
image_xscale = lastDirection;	
