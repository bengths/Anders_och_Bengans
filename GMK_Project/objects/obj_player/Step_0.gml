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
	
	//if(abs(walkSpeed)>maxWalkVel && hDirection != 0) walkSpeed = maxWalkVel*hDirection;
	//xVel += walkSpeed;
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


/*
if(place_meeting(x,y+fallVel,obj_block)) {
	jumpVel = 0;
	fallVel = 0;	
}

// Check for floor benath player
if(place_meeting(x,y+1,obj_block)) {
fallVel = 0;
// Jump
yVel = jumpAcc * key_up;
// Update horizontal speed
if(abs(walkSpeed)<maxWalkVel) walkSpeed += hDirection * walkInvFriction;
if(abs(walkSpeed)>maxWalkVel && hDirection != 0) walkSpeed = maxWalkVel*hDirection;
// Check for walls
if(place_meeting(x+walkSpeed,y-1,obj_block) && walkSpeed != 0) walkSpeed = 0;
// Horizontal speed loss
if(hDirection == 0 && walkSpeed != 0) {
	walkSpeed -= walkInvFriction*walkStopFriction*sign(xVel);
	// Stop oscillations if vel = 0 overidden
	if(walkSpeed *(-1)*sign(xVel)>0) walkSpeed = 0;
}

}


if(fallVel - jumpVel < maxFallVel && !(place_meeting(x,y+fallVel,obj_block))) fallVel += fallAcc;



// Update kinematics
yVel =  fallVel - jumpVel;
xVel = walkSpeed;
if(!place_meeting(x,y+yVel,obj_block)) y += yVel;
if(!place_meeting(x+xVel,y,obj_block)) x += walkSpeed;

