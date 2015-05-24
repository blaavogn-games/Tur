using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour,AlarmListener {
    enum actions { TURN, MOVE, PAUSE };
    GridHandler gridHandler;
    Tile tile, targetTile;
    Vector2 startPos;
    private Vector2 startDirection,direction, targetPosition;
    private const float speed = 3.5f, rotSpeed = 350f;
    private int targetRotation, totalRotation, rotationDirection, startRotation;
    private Alarm alarm;
    private actions action;
    public bool oplyst = true;
    private Stone stone;
    public AudioClip sfxMove, sfxTurn, sfxTurnShort;

	// Use this for initialization
	void Start () {
        gridHandler = GameObject.FindGameObjectWithTag("GameController").GetComponent<GridHandler>();
        Vector2 pos = transform.position;
        pos.x = (pos.x + .25f) - ((pos.x + .25f) % .5f);
        pos.y = (pos.y + .25f) - ((pos.y + .25f) % .5f);
        transform.position = pos;
        startPos = pos;
        tile = gridHandler.getTile(pos);
        alarm = GetComponent<Alarm>();
        alarm.setListener(this);
        startRotation = (int) transform.rotation.eulerAngles.z;

        if (startRotation == 0)
            direction = Vector2.up;
        else if (startRotation == 180)
            direction = Vector2.up * -1;
        else if (startRotation == 90)
            direction = Vector2.right * -1;
        else
            direction = Vector2.right;

        startDirection = direction;
        action = actions.MOVE;
    }

    public void reset() {
        direction = startDirection;
        transform.position = startPos;
        transform.rotation = Quaternion.Euler(0,0,startRotation);
        tile = gridHandler.getTile(transform.position);
        targetRotation = startRotation;
        action = actions.MOVE;
    }

	// Update is called once per frame
    void Update() {
        if (gridHandler.getGameState() == GridHandler.gameStates.RUN) {
            switch(action){
                case actions.MOVE:
                    if (targetTile == null || targetTile.getType() == Tile.Types.WALL) {
                        gridHandler.toggleGameState();
                        return;
                    }

                    Vector2 newPosition = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime); 
                    if(stone != null){
                        stone.transform.Translate(newPosition - (Vector2)transform.position);
                    }
                    transform.position = newPosition;
                    if (((Vector2)transform.position - targetPosition).sqrMagnitude == 0) {
                        tile = targetTile;
                        alarm.addTimer(0.2f, 0, false);
                        action = actions.PAUSE;
                        gridHandler.saveGameState(transform.position, direction);
                        audio.PlayOneShot(sfxMove);
                    }
                break;
                case actions.TURN:
                    //I have 0 idea of what this i doing
                    transform.Rotate(new Vector3(0,0,rotationDirection * rotSpeed * Time.deltaTime));
                    if (oplyst)
                        tile.transform.Rotate(new Vector3(0, 0, rotationDirection * rotSpeed * Time.deltaTime));

                    if (Mathf.Abs(transform.rotation.eulerAngles.z - targetRotation) < 8f || Mathf.Abs(transform.rotation.eulerAngles.z - targetRotation + 360) < 8f) {
                        transform.rotation = Quaternion.Euler(new Vector3(0,0,targetRotation));
                        action = actions.MOVE;
                        if (oplyst && tile.getType() != Tile.Types.DEFAULT) { 
                            tile.transform.rotation = Quaternion.identity;
                            tile.setType(totalRotation);
                        }
                        if (targetTile == null || targetTile.getType() == Tile.Types.WALL) {
                            gridHandler.toggleGameState();
                        }
                    }
                break;
            }
        }
    }

    public void findTarget() {
        targetTile = gridHandler.getTile((Vector2) transform.position + direction * .5f);
        if(action == actions.PAUSE)
            action = actions.MOVE;
        if (targetTile != null  ) {
            targetPosition = targetTile.transform.position;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 0.5f);
            Debug.DrawRay(transform.position, direction * 0.5f, Color.red, 1);
            if (hit) {
                stone = hit.transform.GetComponent<Stone>();
                
                Tile targetTile2 = gridHandler.getTile((Vector2) transform.position + direction);
                RaycastHit2D hit2 = Physics2D.Raycast((Vector2) stone.transform.position + direction * 0.2f, direction, 0.5f);
                Debug.DrawRay((Vector2)stone.transform.position + direction * 0.2f, direction * 0.5f, Color.red, 1);
               
                if (hit2 || targetTile2 == null || targetTile2.getType() == Tile.Types.WALL) {
                    targetTile = null;
                }

            } else {
                stone = null;
            }
        }
    }

    private void findDirection() {
        Vector2 oldDirection = direction;
        rotationDirection = 1;
        switch (tile.getType()) {
            case Tile.Types.UP:
                direction = Vector2.up;
                targetRotation = 0;
                if (transform.rotation.eulerAngles.z < 180)
                    rotationDirection = -1;
                break;
            case Tile.Types.DOWN:
                direction = Vector2.up * -1;
                targetRotation = 180;
                if (transform.rotation.eulerAngles.z > 180)
                    rotationDirection = -1;
                break;
            case Tile.Types.RIGHT:
                direction = Vector2.right;
                targetRotation = 270;
                if (transform.rotation.eulerAngles.z < 90 || transform.rotation.eulerAngles.z > 270)
                    rotationDirection = -1;
                break;
            case Tile.Types.LEFT:
                direction = Vector2.right * -1;
                targetRotation = 90;
                if (transform.rotation.eulerAngles.z < 270 && transform.rotation.eulerAngles.z > 90)
                    rotationDirection = -1;
                break;
        }
        if (oldDirection != direction) { //Learn rotation math
            action = actions.TURN;
            totalRotation = Mathf.RoundToInt(targetRotation - transform.rotation.eulerAngles.z);
            if (totalRotation < 0)
                totalRotation += 360;
            if(totalRotation == 180)
                audio.PlayOneShot(sfxTurn);
            else
                audio.PlayOneShot(sfxTurnShort);
        }
    }

    public void onAlarm(int i) {
        switch(i){
            case 0:
                if (tile.getType() != Tile.Types.DEFAULT) {
                    findDirection();
                }
                findTarget();
                break;
        }
    }
}
