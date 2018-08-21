using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PilotController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private RectTransform uiPanel;
    private Transform cameraRotator;
    private Transform cameraPivot; 
    [SerializeField] private float posDamp;
    [SerializeField] private float rotDamp;
    [SerializeField] private Transform cameraView;
    private Animator animator;
    [SerializeField] private bool fly = true;
	private bool _fly;
    private Vector3 pilotStartPosition;
    private Vector3 cameraStartPosition;

    private Vector3 localPos;
    private Quaternion rot;
    private float turn;

    void Awake()
    {
        _fly = !fly;
        cameraRotator = cameraTransform.GetComponentInParent<ObjectRotation>().transform;
        cameraPivot = cameraTransform.parent;
        cameraStartPosition = cameraTransform.localPosition;
    }
    IEnumerator Start()
    {
        pilotStartPosition = transform.localPosition;

        animator = GetComponent<Animator>();
        Input.gyro.enabled = true;
		localPos = cameraTransform.position.ToLocalPosition(transform);
        while (true)
        {
            yield return new WaitForEndOfFrame();
            float time = 0.0f;
            float dTime = Random.Range(5.0f, 7.0f);
            while (Mathf.Abs(turn) < 0.1f && time < dTime)
            {
                yield return new WaitForEndOfFrame();
                time += Time.deltaTime;
            }
            if (Mathf.Abs(turn) < 0.1f)
            {
                animator.SetBool("revert", true);
            }
            time = 0.0f;
            dTime = Random.Range(3.0f, 5.0f);
            while (Mathf.Abs(turn) < 0.1f && time < dTime)
            {
                yield return new WaitForEndOfFrame();
                time += Time.deltaTime;
            }
            animator.SetBool("revert", false);
        }
    }

    void FixedUpdate()
    {
        turn = (Input.gyro.gravity.x + Input.GetAxis("Horizontal"));
        animator.SetFloat("turn", turn);
		if (fly)
        {
            transform.Rotate(10.0f * Vector3.up * Time.fixedDeltaTime * turn);
            transform.position += speed * Time.fixedDeltaTime * (transform.forward - 0.25f * Vector3.up);

			cameraTransform.position = Vector3.Lerp(cameraTransform.position, localPos.ToWordPosition(transform), posDamp * Time.fixedDeltaTime);
            rot.SetLookRotation(cameraView.forward);
			cameraTransform.rotation = Quaternion.Lerp(cameraTransform.rotation, rot, rotDamp * Time.fixedDeltaTime);
        }
		if (_fly != fly)
		{
            _fly = fly;
            if (fly)
            {
                cameraPivot.parent = cameraRotator.parent;
                uiPanel.gameObject.SetActive(false);
            }
            else
            {
                cameraPivot.parent = cameraRotator;
                transform.localPosition = pilotStartPosition;
                transform.localRotation = Quaternion.identity;
                cameraTransform.localPosition = cameraStartPosition;
                cameraTransform.localRotation = Quaternion.identity;
                uiPanel.gameObject.SetActive(true);
            }
		}
    }
}
