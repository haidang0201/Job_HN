using UnityEngine;
using System.Collections;

public class PlayerInteraction : MonoBehaviour
{
    private IInteractable current;

    private Animator animator;

    private bool isInteracting;


    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }


    // =========================
    // PLAYER ĐI VÀO VÙNG TƯƠNG TÁC
    // =========================
    private void OnTriggerEnter(Collider other)
    {
        if (isInteracting)
            return;


        IInteractable interactable =
            other.GetComponentInParent<IInteractable>();

        if (interactable == null)
            return;


        current = interactable;


        // ===== DOOR =====
        Door door =
            other.GetComponentInParent<Door>();

        if (door != null)
        {
            UIManager.Instance.ShowDoorIcon();
            return;
        }


        // ===== PLATFORM BUTTON =====
        PlatformButton platformButton =
            other.GetComponentInParent<PlatformButton>();

        if (platformButton != null)
        {
            UIManager.Instance.ShowPlatformIcon();
        }
    }


    // =========================
    // PLAYER RỜI VÙNG TƯƠNG TÁC
    // =========================
    private void OnTriggerExit(Collider other)
    {
        if (isInteracting)
            return;


        IInteractable interactable =
            other.GetComponentInParent<IInteractable>();


        if (interactable == current)
        {
            current = null;

            UIManager.Instance.HideAll();
        }
    }


    // =========================
    // UI BUTTON GỌI HÀM NÀY
    // =========================
    public void Interact()
    {
        if (current == null || isInteracting)
            return;


        // ===== DOOR =====

        Door door = current as Door;

        if (door != null)
        {
            StartCoroutine(OpenDoor(door));
            return;
        }


        // ===== PLATFORM =====

        PlatformButton platformButton =
            current as PlatformButton;

        if (platformButton != null)
        {
            StartCoroutine(OpenPlatform(platformButton));
        }
    }


    // =========================
    // MỞ CỬA
    // =========================
    private IEnumerator OpenDoor(Door door)
    {
        isInteracting = true;


        // Ẩn icon
        UIManager.Instance.HideAll();


        // Player chạy animation mở cửa
        animator.SetTrigger("Open");


        yield return null;


        // Chờ vào state Open_door
        while (!animator
            .GetCurrentAnimatorStateInfo(0)
            .IsName("Open_door"))
        {
            yield return null;
        }


        // Chờ animation chạy xong
        while (animator
            .GetCurrentAnimatorStateInfo(0)
            .normalizedTime < 1f)
        {
            yield return null;
        }


        // Ẩn cửa
        door.HideDoor();


        current = null;

        isInteracting = false;
    }


    // =========================
    // MỞ PLATFORM
    // =========================
    private IEnumerator OpenPlatform(
        PlatformButton platformButton)
    {
        isInteracting = true;


        // Ẩn Platform Icon
        UIManager.Instance.HideAll();


        // Player chạy animation Working
        animator.SetTrigger("Working");


        yield return null;


        // Chờ Animator vào animation
        while (!animator
            .GetCurrentAnimatorStateInfo(0)
            .IsName("Working On Device"))
        {
            yield return null;
        }


        // Chờ animation chạy xong
        while (animator
            .GetCurrentAnimatorStateInfo(0)
            .normalizedTime < 1f)
        {
            yield return null;
        }


        // Animation xong
        // → hiện Platform
        // → ẩn Button3D
        platformButton.Interact();


        current = null;

        isInteracting = false;
    }
}